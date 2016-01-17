using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    /// <summary>
    /// Represents the result of a download process.
    /// </summary>
    public class DownloadResult : AbstractFileProcessingResult
    {
        public DownloadResult(long bytesDownloaded) : base(bytesDownloaded) { }
        public DownloadResult(long bytesDownloaded, Exception error) : base(bytesDownloaded, error) { }
    }
}
