using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public class DownloadResult : AbstractFileProcessingResult
    {
        public DownloadResult(long bytesDownloaded) : base(bytesDownloaded) { }
        public DownloadResult(long bytesDownloaded, Exception error) : base(bytesDownloaded, error) { }
    }
}
