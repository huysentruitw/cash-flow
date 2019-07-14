#if false
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using CashFlow.Command.Repositories;
using Microsoft.EntityFrameworkCore.Internal;

namespace CashFlow.Importing
{
    public class ImportService
    {
        private readonly ICodeRepository _codeRepository;
        private readonly ITransactionRepository _transactionRepository;

        public ImportService(ICodeRepository codeRepository, ITransactionRepository transactionRepository)
        {
            _codeRepository = codeRepository;
            _transactionRepository = transactionRepository;
        }

        public void Import()
        {
            string fileName = @"D:\temp\dagboek.txt";
            var lines = new Queue<string>(File.ReadAllLines(fileName));

            lines.Dequeue(); // Headers

            string account = null;

            var transactions = new List<Transaction>();

            int sequence = 0;
            while (lines.Any())
            {
                string line = lines.Dequeue().Trim();
                if (line == string.Empty)
                {
                    if (!lines.Any())
                        break;

                    line = lines.Dequeue().Trim();
                    account = line;
                    continue;
                }

                string[] parts = line.Split('\t');

                if (parts.Length < 2)
                    continue;

                transactions.Add(ParseTransaction(sequence++, account, parts));
            }

            var codes = transactions
                .Select(x => x.Code1)
                .Concat(transactions.Select(x => x.Code2))
                .Where(x => x.Length > 0)
                .Distinct();

            //foreach (var code in codes)
            //{
            //    _codeRepository.AddCode(code).Wait();
            //}

            foreach (var transaction in transactions
                .OrderBy(x => x.TransactionDateUtc)
                .GroupBy(x => x.TransactionDateUtc)
                .SelectMany(group =>
                {
                    int count = group.Count();
                    var sameDayTransactions = group.OrderBy(x => x.Sequence).ToList();
                    var result = new List<Transaction>();

                    while (sameDayTransactions.Any())
                    {
                        var transaction = sameDayTransactions[0];
                        sameDayTransactions.RemoveAt(0);

                        if (!IsInternalTransfer(transaction))
                        {
                            result.Add(transaction);
                            continue;
                        }

                        var transfer = transaction;
                        int i = 0;
                        while (sameDayTransactions.Any())
                        {
                            transaction = sameDayTransactions[i];
                            sameDayTransactions.RemoveAt(i);

                            if (transaction.Account == transfer.Account)
                            {
                                sameDayTransactions.Insert(i, transaction);
                                i++;
                                continue;
                            }

                            if (!IsInternalTransfer(transaction))
                            {
                                result.Add(transaction);
                                continue;
                            }

                            if (transaction.AmountInCents != -transfer.AmountInCents)
                            {
                                sameDayTransactions.Insert(i, transaction);
                                i++;
                                continue;
                            }

                            if (transfer.AmountInCents < 0)
                            {
                                result.Add(transfer);
                            }
                            result.Add(transaction);
                            if (transfer.AmountInCents >= 0)
                            {
                                result.Add(transfer);
                            }
                            transfer = null;
                            break;
                        }

                        Debug.Assert(transfer == null);
                    }

                    Debug.Assert(count == result.Count());
                    Debug.Assert(sameDayTransactions.Count() == 0);
                    return result;
                }))
            {
                bool isInternalTransfer = false;
                if (transaction.Code1 == "Overdracht")
                {
                    transaction.Code1 = "";
                    isInternalTransfer = true;
                }
                if (transaction.Code2 == "Overdracht")
                {
                    transaction.Code2 = "";
                    isInternalTransfer = true;
                }

                var codeNames = (new string[] { transaction.Code1, transaction.Code2 })
                    .Distinct()
                    .Where(x => x.Length > 0)
                    .ToArray();

                _transactionRepository.Add(
                    id: Guid.NewGuid(),
                    financialYearId: ToFinancialYearId(transaction.TransactionDateUtc.Year),
                    transactionDate: transaction.TransactionDateUtc,
                    accountId: ToAccountId(transaction.Account),
                    supplierId: null,
                    amountInCents: transaction.AmountInCents,
                    isInternalTransfer: isInternalTransfer,
                    description: transaction.Description,
                    comment: null,
                    codeNames: codeNames).Wait();

                Thread.Sleep(1);
            }
        }

        private static bool IsInternalTransfer(Transaction transaction)
            => transaction.Code1 == "Overdracht" || transaction.Code2 == "Overdracht";

        private static Transaction ParseTransaction(int sequence, string account, string[] parts)
        {
            var utc = DateTime.SpecifyKind(DateTime.Parse(parts[0]), DateTimeKind.Utc);
            return new Transaction
            {
                Account = account,
                Sequence = sequence,
                TransactionDateUtc = utc,
                EvidenceNumber = parts[1].Trim(),
                Code1 = parts[2].Trim(),
                Code2 = parts[3].Trim(),
                Description = parts[4].Trim(),
                AmountInCents = ParseAmount(parts[5].Trim(), parts[6].Trim()),
            };
        }

        private static Guid ToAccountId(string account)
        {
            switch (account)
            {
            case "Cash": return new Guid("9DE4B69A-79C4-4613-B2C6-C2145979A158");
            case "Zichtrekening": return new Guid("4612DC6D-708F-441F-BD29-50D955221D88");
            case "Spaarrekening": return new Guid("6FA8F317-11BC-40C5-8C3B-C5895CF5E9F4");
            default: throw new InvalidOperationException($"Unknown account {account}");
            }
        }

        private static Guid ToFinancialYearId(int year)
        {
            switch (year)
            {
            case 2017: return new Guid("929729ba-01e6-43ba-a440-95e32721ed52");
            case 2018: return new Guid("1cb09dae-dedd-413b-b6ac-08a52901fb92");
            case 2019: return new Guid("83e4c79f-90d8-4798-a0f3-8cac527dffd5");
            default:
                throw new InvalidOperationException($"Unknown financial year {year}");
            }
        }

        private static long ParseAmount(string credit, string debit)
        {
            credit = credit.Replace("€", string.Empty).Replace(".", string.Empty).Trim();
            debit = debit.Replace("€", string.Empty).Replace(".", string.Empty).Trim();

            var c = decimal.TryParse(credit, out var cc) ? cc : 0;
            var d = decimal.TryParse(debit, out var dd) ? dd : 0;
            return (long)((c - d) * 100);
        }

        private class Transaction
        {
            public string Account { get; set; }

            public int Sequence { get; set; }

            public DateTime TransactionDateUtc { get; set; }

            public string EvidenceNumber { get; set; }

            public string Code1 { get; set; }

            public string Code2 { get; set; }

            public string Description { get; set; }

            public long AmountInCents { get; set; }
        }
    }
}
#endif
