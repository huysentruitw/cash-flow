using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions.Entities;
using CashFlow.Query.Abstractions.Repositories;
using CashFlow.Reporting.Templates;

namespace CashFlow.Reporting.Services
{
    internal sealed class ReportService : IReportService
    {
        private readonly IPdfGenerator _pdfGenerator;
        private readonly IAccountRepository _accountRepository;
        private readonly ICodeBalanceRepository _codeBalanceRepository;
        private readonly IFinancialYearRepository _financialYearRepository;

        public ReportService(
            IPdfGenerator pdfGenerator,
            IAccountRepository accountRepository,
            ICodeBalanceRepository codeBalanceRepository,
            IFinancialYearRepository financialYearRepository)
        {
            _pdfGenerator = pdfGenerator;
            _accountRepository = accountRepository;
            _codeBalanceRepository = codeBalanceRepository;
            _financialYearRepository = financialYearRepository;
        }

        public async Task<Stream> GenerateByCodeOverviewPdf(string codeName, Guid? financialYearId = null)
        {
            IReadOnlyDictionary<Guid, string> accounts =
                (await _accountRepository.GetAccounts()).ToDictionary(x => x.Id, x => x.Name);
            Func<Guid, string> accountNameResolver =
                accountId => accounts.TryGetValue(accountId, out string accountName) ? accountName : null;

            FinancialYear financialYear = financialYearId.HasValue
                ? await _financialYearRepository.GetFinancialYear(financialYearId.Value)
                : null;
            Transaction[] transactions = await _codeBalanceRepository.GetCodeTransactions(financialYearId, codeName);

            var data = new TemplateData
            {
                FinancialYear = financialYear?.Name,
                CodeName = codeName,
                Transactions = MapTransactions(transactions, accountNameResolver),
            };

            return await _pdfGenerator.GeneratePdf(
                bodyTemplate: LoadTemplate("ByCodeOverview.html"),
                templateData: data,
                footer: "[date] - [page]/[toPage]",
                margins: new PageMargins(10, 10, 15, 10));
        }

        private static string LoadTemplate(string templateName)
        {
            using (Stream stream = typeof(ITemplatesMarker).Assembly.GetManifestResourceStream(typeof(ITemplatesMarker), templateName))
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        private static IEnumerable<TransactionData> MapTransactions(Transaction[] transactions, Func<Guid, string> accountNameResolver)
        {
            long balanceInCents = 0;
            foreach (Transaction transaction in transactions)
            {
                balanceInCents += transaction.AmountInCents;
                yield return MapTransaction(transaction, balanceInCents, accountNameResolver);
            }
        }

        private static TransactionData MapTransaction(Transaction transaction, long balanceInCents, Func<Guid, string> accountNameResolver)
            => new TransactionData
            {
                TransactionDate = $"{transaction.TransactionDate:dd-MM-yyyy}",
                AccountBadge = accountNameResolver(transaction.AccountId)?[0],
                Description = transaction.Description,
                Income = transaction.AmountInCents > 0 ? $"{transaction.AmountInCents / 100:F2}" : null,
                Expense = transaction.AmountInCents < 0 ? $"{-transaction.AmountInCents / 100:F2}" : null,
                Balance = $"{balanceInCents / 100:F2}",
            };

        private sealed class TemplateData
        {
            public string FinancialYear { get; set; }

            public string CodeName { get; set; }

            public IEnumerable<TransactionData> Transactions { get; set; }
        }

        private sealed class TransactionData
        {
            public string TransactionDate { get; set; }

            public char? AccountBadge { get; set; }

            public string Description { get; set; }

            public string Income { get; set; }

            public string Expense { get; set; }

            public string Balance { get; set; }
        }
    }
}
