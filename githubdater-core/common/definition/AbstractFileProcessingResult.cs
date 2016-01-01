using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public abstract class AbstractFileProcessingResult : IFileProcessingResult
    {
        private readonly long bytesProcessed;
        private readonly Exception error;

        public AbstractFileProcessingResult(long bytesProcessed) : this(bytesProcessed, null) { }

        public AbstractFileProcessingResult(long bytesProcessed, Exception error)
        {
            this.bytesProcessed = bytesProcessed;
            this.error = error;
        }

        public bool IsSuccess { get { return (error == null); } }
        public long BytesProcessed { get { return bytesProcessed; } }
        public Exception Error { get { return error; } }
    }
}
