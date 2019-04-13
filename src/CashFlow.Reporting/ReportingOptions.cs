using System;

namespace CashFlow.Reporting
{
    public sealed class ReportingOptions
    {
        public Uri JsReportServiceUri { get; set; }

        public void Validate()
        {
            if (JsReportServiceUri == null)
                throw new ArgumentNullException(nameof(JsReportServiceUri));
        }
    }
}
