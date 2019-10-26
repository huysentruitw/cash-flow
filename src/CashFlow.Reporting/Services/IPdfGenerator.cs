using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CashFlow.Reporting.Services
{
    internal interface IPdfGenerator
    {
        Task<Stream> GeneratePdf(
            string bodyTemplate,
            object templateData = null,
            string header = null,
            string footer = null,
            PageMargins margins = null,
            PageFormat format = PageFormat.A4,
            PageOrientation orientation = PageOrientation.Portrait,
            CancellationToken cancellationToken = default);
    }

    internal enum PageFormat
    {
        A4,
        Letter
    }

    internal sealed class PageMargins
    {
        public PageMargins(int topInMm, int rightInMm, int bottomInMm, int leftInMm)
        {
            TopInMm = topInMm;
            RightInMm = rightInMm;
            BottomInMm = bottomInMm;
            LeftInMm = leftInMm;
        }

        public PageMargins(int topBottomInMm, int rightLeftInMm)
        {
            TopInMm = BottomInMm = topBottomInMm;
            RightInMm = LeftInMm = rightLeftInMm;
        }

        public int? TopInMm { get; set; }

        public int? RightInMm { get; set; }

        public int? BottomInMm { get; set; }

        public int? LeftInMm { get; set; }
    }

    internal enum PageOrientation
    {
        Portrait,
        Landscape
    }
}
