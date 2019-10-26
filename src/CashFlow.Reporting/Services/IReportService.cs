using System;
using System.IO;
using System.Threading.Tasks;

namespace CashFlow.Reporting.Services
{
    public interface IReportService
    {
        Task<Stream> GenerateByCodeOverviewPdf(string codeName, Guid? financialYearId = null);
    }
}
