using System;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using CashFlow.Data.Abstractions.Entities;
using CashFlow.Query.Abstractions.Repositories;
using CashFlow.Reporting.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CashFlow.Reporting.Controllers
{
    [ApiController]
    [Route("api/report")]
    public sealed class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IFinancialYearRepository _financialYearRepository;

        public ReportController(
            IReportService reportService,
            IFinancialYearRepository financialYearRepository)
        {
            _reportService = reportService;
            _financialYearRepository = financialYearRepository;
        }

        [HttpGet]
        [Route("financial-year/{financialYearId}/overview")]
        public IActionResult FinancialYearOverview(Guid financialYearId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("financial-year/{financialYearId}/by-code-overview/{codeName}")]
        public async Task<IActionResult> FinancialYearByCodeOverview(Guid financialYearId, string codeName)
        {
            FinancialYear financialYear = await _financialYearRepository.GetFinancialYear(financialYearId);
            string financialYearName = financialYear?.Name ?? string.Empty;
            Stream stream = await _reportService.GenerateByCodeOverviewPdf(codeName, financialYearId);
            return new InlineFileStreamResult(stream, "application/pdf")
            {
                FileDownloadName = $"{DateTime.Now:yyyy-MM-dd--HH-mm-ss} CashFlow-{financialYearName}-{codeName}.pdf",
            };
        }

        [HttpGet]
        [Route("by-code-overview/{codeName}")]
        public async Task<IActionResult> ByCodeOverview(string codeName)
        {
            Stream stream = await _reportService.GenerateByCodeOverviewPdf(codeName);
            return new FileStreamResult(stream, "application/pdf")
            {
                FileDownloadName = $"{DateTime.Now:yyyy-MM-dd--HH-mm-ss} CashFlow-Overall-{codeName}.pdf",
            };
        }

        private sealed class InlineFileStreamResult : FileStreamResult
        {
            public InlineFileStreamResult(Stream fileStream, string contentType)
                : base(fileStream, contentType)
            {
            }

            public override Task ExecuteResultAsync(ActionContext context)
            {
                var contentDispositionHeader = new ContentDisposition
                {
                    FileName = FileDownloadName,
                    Inline = true,
                };
                context.HttpContext.Response.Headers.Add("Content-Disposition", contentDispositionHeader.ToString());
                context.HttpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                FileDownloadName = null;
                return base.ExecuteResultAsync(context);
            }
        }
    }
}
