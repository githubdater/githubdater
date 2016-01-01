using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public class DownloadProgress : FileProcessingProgress
    {
        public DownloadProgress(string title, long bytesProcessed, long totalBytesToProcess) 
            : base(title, bytesProcessed, totalBytesToProcess) { }

        public DownloadProgress(string title, long bytesProcessed, long totalBytesToProcess, Exception error)
            : base(title, bytesProcessed, totalBytesToProcess, error) { }
    }
}
