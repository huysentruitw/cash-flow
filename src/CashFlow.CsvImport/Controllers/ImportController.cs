using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions;
using CashFlow.Data.Abstractions.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.CsvImport.Controllers
{
    [ApiController]
    [Route("api/import")]
    [SuppressMessage("ReSharper", "PossiblyMistakenUseOfParamsMethod")]
    public sealed class ImportController : ControllerBase
    {
        private readonly IDataContext _dataContext;

        public ImportController(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("financial-year/{financialYearId}/account/{accountId}/transactions")]
        public async Task<IActionResult> ImportTransactions(
            Guid financialYearId,
            Guid accountId,
            [FromForm] IFormFile csvFile,
            CancellationToken cancellationToken)
        {
            if (csvFile == null) return NotFound();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = "\n",
                HasHeaderRecord = true,
                Delimiter = ";",
                Encoding = Encoding.UTF8,
            };

            using var reader = new StreamReader(csvFile.OpenReadStream());
            using var csv = new CsvReader(reader, config);
            CrelanCsvTransaction[] csvTransactions = csv.GetRecords<CrelanCsvTransaction>().ToArray();

            DateTimeOffset utcNow = DateTimeOffset.UtcNow;
            var dateOffset = TimeSpan.FromMilliseconds(1);

            foreach (CrelanCsvTransaction csvTransaction in csvTransactions)
            {
                DateTime transactionDate = DateTime.SpecifyKind(csvTransaction.TransactionDate, DateTimeKind.Utc) + dateOffset;

                var transaction = new Transaction
                {
                    Id = Guid.NewGuid(),
                    FinancialYearId = financialYearId,
                    TransactionDate = transactionDate,
                    AccountId = accountId,
                    SupplierId = null,
                    DateCreated = utcNow,
                    AmountInCents = (int)(csvTransaction.Amount * 100),
                    IsInternalTransfer = false,
                    Description = csvTransaction.TransactionType.StartsWith("Kosten") || csvTransaction.TransactionType.StartsWith("Beheren")
                        ? csvTransaction.TransactionType
                        : $"{csvTransaction.ThirdParty} | {csvTransaction.Description}",
                    Comment = string.Empty,
                };

                await _dataContext.Transactions.AddAsync(transaction, cancellationToken);

                dateOffset += TimeSpan.FromMilliseconds(1);
            }

            await _dataContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        private sealed class CrelanCsvTransaction
        {
            [Name("Datum")]
            [Format("dd/MM/yyyy")]
            public DateTime TransactionDate { get; set; }

            [Name("Bedrag")]
            public decimal Amount { get; set; }

            [Name("Munt")]
            public string Currency { get; set; }

            [Name("Tegenpartij")]
            public string ThirdParty { get; set; }

            [Name("Type verrichting")]
            public string TransactionType { get; set; }

            [Name("Mededeling")]
            public string Description { get; set; }
        }
    }
}
