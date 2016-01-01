using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public class FileProcessingProgress : IProgress
    {
        private readonly string title;
        private readonly long bytesProcessed;
        private readonly long totalBytesToProcess;
        private readonly Exception error;

        public FileProcessingProgress(string title, long bytesProcessed, long totalBytesToProcess) 
            : this(title, bytesProcessed, totalBytesToProcess, null) { }

        public FileProcessingProgress(string title, long bytesProcessed, long totalBytesToProcess, Exception error)
        {
            if (title == null || title == string.Empty)
                throw new ArgumentNullException("Title of the file processing progress can't be empty.");

            this.title = title;
            this.bytesProcessed = bytesProcessed;
            this.totalBytesToProcess = totalBytesToProcess;
            this.error = error;
        }

        public string Title { get { return title; } }
        public long BytesProcessed { get { return bytesProcessed; } }
        public long TotalBytesToProcess { get { return totalBytesToProcess; } }

        public int Percentage
        {
            get {
                if (totalBytesToProcess == 0)
                    return 0;

                decimal ratio = (decimal)bytesProcessed / (decimal)totalBytesToProcess;
                decimal percentage = ratio * (decimal)100;
                return (int)percentage;
            }
        }

        public bool Done { get { return bytesProcessed >= totalBytesToProcess; } }
        public Exception Error { get { return error; } }
    }
}
