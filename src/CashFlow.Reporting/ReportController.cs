using System;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Reporting
{
    [ApiController]
    [Route("api/report")]
    public sealed class ReportController : ControllerBase
    {
        private readonly IReportGenerator _reportGenerator;

        public ReportController(IReportGenerator reportGenerator)
        {
            _reportGenerator = reportGenerator;
        }

        [HttpGet]
        [Route("financial-year/{financialYearId}/overview")]
        public IActionResult FinancialYearOverview(Guid financialYearId)
        {
            throw new NotImplementedException();
        }
    }
}
