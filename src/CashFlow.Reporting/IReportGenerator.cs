using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CashFlow.Reporting
{
    public interface IReportGenerator
    {
        Task<Stream> GeneratePdf(
            string bodyTemplate,
            string headerTemplate = null,
            string footerTemplate = null,
            object templateData = null,
            PageMargins margins = null,
            PageFormat format = PageFormat.A4,
            PageOrientation orientation = PageOrientation.Portrait,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
