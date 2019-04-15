using System.IO;
using System.Threading;
using System.Threading.Tasks;
using jsreport.Shared;
using jsreport.Types;

namespace CashFlow.Reporting
{
    internal sealed class ReportGenerator : IReportGenerator
    {
        private readonly IRenderService _renderService;
        private readonly string _handlebarsHelpers;

        public ReportGenerator(IRenderService renderService)
        {
            _renderService = renderService;
            _handlebarsHelpers = LoadHandlebarsHelpers();
        }

        public async Task<Stream> GeneratePdf(
            string bodyTemplate,
            string headerTemplate = null,
            string footerTemplate = null,
            object templateData = null,
            PageMargins margins = null,
            PageFormat format = PageFormat.A4,
            PageOrientation orientation = PageOrientation.Portrait,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var renderRequest = new RenderRequest
            {
                Template = new Template
                {
                    Engine = Engine.Handlebars,
                    Recipe = Recipe.ChromePdf,
                    Content = bodyTemplate,
                    Helpers = _handlebarsHelpers,
                    Chrome = new Chrome
                    {
                        Landscape = orientation == PageOrientation.Landscape,
                        Format = format.ToString(),
                        DisplayHeaderFooter = true,
                        HeaderTemplate = headerTemplate ?? "&nbsp;",
                        FooterTemplate = footerTemplate ?? "&nbsp;",
                        MarginTop = margins?.TopInPx != null ? $"{margins.TopInPx}px" : null,
                        MarginRight = margins?.RightInPx != null ? $"{margins.RightInPx}px" : null,
                        MarginBottom = margins?.BottomInPx != null ? $"{margins.BottomInPx}px" : null,
                        MarginLeft = margins?.LeftInPx != null ? $"{margins.LeftInPx}px" : null,
                        MediaType = MediaType.Print
                    }
                },
                Data = templateData
            };

            Report report = await _renderService.RenderAsync(renderRequest, cancellationToken);
            return report.Content;
        }

        private static string LoadHandlebarsHelpers()
        {
            using (Stream stream = typeof(ReportGenerator).Assembly.GetManifestResourceStream(typeof(ReportGenerator), "HandlebarsHelpers.js"))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
