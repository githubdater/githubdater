using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    /// <summary>
    /// This class is the default implementation for <see cref="IFileProcessingResult"/>.
    /// It provides an immutable state for properties exposed by <see cref="IFileProcessingResult"/>.
    /// </summary>
    /// <seealso cref="IFileProcessingResult"/>
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

        /// <summary>
        /// Convenience property. Returns <c>true</c> if the <see cref="Error"/> property returns <c>false</c>.
        /// </summary>
        public bool IsSuccess { get { return (error == null); } }
        public long BytesProcessed { get { return bytesProcessed; } }
        public Exception Error { get { return error; } }
    }
}
