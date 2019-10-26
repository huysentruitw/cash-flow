using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using HandlebarsDotNet;

namespace CashFlow.Reporting.Services
{
    internal sealed class PdfGenerator : IPdfGenerator
    {
        private readonly IConverter _converter;
        private readonly IHandlebars _handlebars;

        public PdfGenerator(IConverter converter, IHandlebars handlebars)
        {
            _converter = converter;
            _handlebars = handlebars;
            RegisterHandlebarsHelpers();
        }

        private void RegisterHandlebarsHelpers()
        {
            _handlebars.RegisterHelper("lt", (writer, context, args) =>
            {
                if (args.Length != 2)
                {
                    writer.Write("lt:Requires exactly two arguments");
                    return;
                }

                if (args[0] == null || args[0].GetType().Name == "UndefinedBindingResult")
                {
                    writer.Write("lt:First argument is undefined");
                    return;
                }

                if (args[1] == null || args[1].GetType().Name == "UndefinedBindingResult")
                {
                    writer.Write("lt:Second argument is undefined");
                    return;
                }

                var val1 = double.Parse(args[0].ToString());
                var val2 = double.Parse(args[1].ToString());
                if (val1 < val2)
                    writer.Write(true);
            });
        }

        public Task<Stream> GeneratePdf(
            string bodyTemplate,
            object templateData = null,
            string header = null,
            string footer = null,
            PageMargins margins = null,
            PageFormat format = PageFormat.A4,
            PageOrientation orientation = PageOrientation.Portrait,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(bodyTemplate))
                throw new ArgumentException("Value cannot be null or empty", nameof(bodyTemplate));

            var htmlContent = templateData != null
                ? _handlebars.Compile(bodyTemplate)(templateData)
                : bodyTemplate;

            var document = new HtmlToPdfDocument
            {
                Objects =
                {
                    new ObjectSettings
                    {
                        PagesCount = true,
                        HtmlContent = htmlContent,
                        HeaderSettings =
                        {
                            Center = header ?? string.Empty,
                            FontSize = 8,
                        },
                        FooterSettings =
                        {
                            Center = footer ?? string.Empty,
                            FontSize = 8,
                        },
                        WebSettings =
                        {
                            Background = true,
                            DefaultEncoding = "utf-8",
                        }
                    }
                },
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Margins =
                    {
                        Top = margins?.TopInMm,
                        Right = margins?.RightInMm,
                        Bottom = margins?.BottomInMm,
                        Left = margins?.LeftInMm,
                    },
                    Orientation = MapOrientation(orientation),
                    PaperSize = MapPageFormat(format),
                },
            };

            return Task.FromResult<Stream>(new MemoryStream(_converter.Convert(document)));
        }

        private static Orientation MapOrientation(PageOrientation orientation)
            => orientation == PageOrientation.Portrait ? Orientation.Portrait : Orientation.Landscape;

        private static PaperKind MapPageFormat(PageFormat format)
        {
            switch (format)
            {
            case PageFormat.A4:
                return PaperKind.A4;
            case PageFormat.Letter:
                return PaperKind.Letter;
            }

            throw new ArgumentOutOfRangeException(nameof(format));
        }
    }
}
