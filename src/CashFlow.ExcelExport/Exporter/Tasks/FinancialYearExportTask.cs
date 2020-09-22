using System;
using System.Collections.Generic;
using System.Linq;
using CashFlow.Data.Abstractions;
using CashFlow.Data.Abstractions.Entities;
using CashFlow.ExcelExport.Exceptions;
using Exportr;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.ExcelExport.Exporter.Tasks
{
    internal sealed class FinancialYearExportTask : IExportTask
    {
        private readonly IDataContext _dataContext;
        private readonly FinancialYear _financialYear;

        public FinancialYearExportTask(IDataContext dataContext, Guid financialYearId)
        {
            _dataContext = dataContext;
            _financialYear = _dataContext.FinancialYears
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == financialYearId)
                ?? throw new FinancialYearNotFoundException(financialYearId);
        }

        public string Name => $"Financial Year {_financialYear.Name}";

        public IEnumerable<ISheetExportTask> EnumSheetExportTasks()
        {
            yield return new TransactionsSheetExportTask(_dataContext, _financialYear);
        }
    }

    internal sealed class TransactionsSheetExportTask : ISheetExportTask
    {
        private readonly IDataContext _dataContext;
        private readonly FinancialYear _financialYear;
        private readonly Account[] _accounts;

        public TransactionsSheetExportTask(IDataContext dataContext, FinancialYear financialYear)
        {
            _dataContext = dataContext;
            _financialYear = financialYear;
            _accounts = _dataContext.Accounts
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToArray();
        }

        public string Name => "Transactions";

        public string[] GetColumnLabels()
        {
            IEnumerable<string> EnumColumnLabels()
            {
                yield return "Date";

                foreach (Account account in _accounts)
                {
                    yield return $"{account.Name} credit";
                    yield return $"{account.Name} debit";
                    yield return $"{account.Name} balance";
                }

                yield return "Evidence number";
                yield return "Description";
            }

            return EnumColumnLabels().ToArray();
        }

        public IEnumerable<IEnumerable<object>> EnumRowData()
        {
            yield return EnumStartingBalanceCells();

            IEnumerable<Transaction> transactions = _dataContext.Transactions
                .AsNoTracking()
                .Where(x => x.FinancialYearId == _financialYear.Id)
                .OrderBy(x => x.TransactionDate)
                .ThenBy(x => x.DateCreated);

            int rowNumber = 3;
            foreach (Transaction transaction in transactions)
            {
                yield return EnumTransactionCells(transaction, rowNumber++);
            }
        }

        private IEnumerable<object> EnumStartingBalanceCells()
        {
            // Date
            yield return null;

            foreach (Account account in _accounts)
            {
                // Credit
                yield return null;
                // Debit
                yield return null;
                // Balance
                yield return _dataContext.StartingBalances
                    .AsNoTracking()
                    .Where(x => x.FinancialYearId == _financialYear.Id && x.AccountId == account.Id)
                    .Select(x => x.StartingBalanceInCents / 100.0M)
                    .FirstOrDefault();
            }

            // Evidence number
            yield return null;
            // Description
            yield return null;
        }

        private IEnumerable<object> EnumTransactionCells(Transaction transaction, int rowNumber)
        {
            yield return transaction.TransactionDate.ToString("yyyy-MM-dd");

            char balanceColumn = 'D';
            foreach (Account account in _accounts)
            {
                if (transaction.AccountId == account.Id)
                {
                    if (transaction.AmountInCents >= 0)
                    {
                        // Credit
                        yield return transaction.AmountInCents / 100.0M;
                        // Debit
                        yield return null;
                    }
                    else
                    {
                        // Credit
                        yield return null;
                        // Debit
                        yield return -transaction.AmountInCents / 100.0M;
                    }
                }
                else
                {
                    // Credit
                    yield return null;
                    // Debit
                    yield return null;
                }

                // Balance formula
                char creditColumn = (char)(balanceColumn - 2);
                char debitColumn = (char)(balanceColumn - 1);
                yield return Formula.Create<int>($"={balanceColumn}{rowNumber - 1} + {creditColumn}{rowNumber} - {debitColumn}{rowNumber}");

                balanceColumn = (char)(balanceColumn + 3);
            }

            yield return transaction.EvidenceNumber;
            yield return transaction.Description;
        }
    }
}
