using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils.Constants
{
    public static class WebConstants
    {
        /// <summary>
        /// OpenTelemetry 的服务名
        /// </summary>
        public const string OpenTelemetryServiceName = "NewsletterApi";

        /// <summary>
        /// OpenTelemetry 的 URL
        /// </summary>
        public const string OpenTelemetryUrl = $"https://localhost:4317";
    }
}
