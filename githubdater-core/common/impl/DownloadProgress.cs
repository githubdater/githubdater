using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    /// <summary>
    /// Represents the progress of a download process.
    /// </summary>
    public class DownloadProgress : FileProcessingProgress
    {
        private const string DownloadingLabel = "Downloading...";

        public DownloadProgress(string title, long bytesProcessed, long totalBytesToProcess) 
            : base(DownloadingLabel + " " + title, bytesProcessed, totalBytesToProcess) { }

        public DownloadProgress(string title, long bytesProcessed, long totalBytesToProcess, Exception error)
            : base(DownloadingLabel + " " + title, bytesProcessed, totalBytesToProcess, error) { }
    }
}
