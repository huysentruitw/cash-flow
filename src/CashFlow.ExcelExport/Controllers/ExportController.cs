using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using CashFlow.Data.Abstractions;
using CashFlow.ExcelExport.Exporter.Tasks;
using Exportr;
using Exportr.OpenXml;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.ExcelExport.Controllers
{
    [ApiController]
    [Route("api/export")]
    [SuppressMessage("ReSharper", "PossiblyMistakenUseOfParamsMethod")]
    public sealed class ExportController : ControllerBase
    {
        private readonly IDataContext _dataContext;

        public ExportController(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        [Route("financial-year/{financialYearId}/transactions")]
        public IActionResult FinancialYearTransactions(Guid financialYearId, CancellationToken cancellationToken)
        {
            var exporter = new FileStreamExporter(
                new ExcelDocumentFactory(),
                new FinancialYearExportTask(_dataContext, financialYearId));

            var stream = new MemoryStream();
            exporter.ExportToStream(stream);
            stream.Position = 0;
            return File(
                fileStream: stream,
                contentType: "application/octet-stream",
                fileDownloadName: exporter.GetFileName());
        }
    }
}
