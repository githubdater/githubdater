using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    /// <summary>
    /// Represents the result of a file processing operation (e.g. downloading, copying...).
    /// </summary>
    public interface IFileProcessingResult
    {
        /// <summary>
        /// The number of bytes processed during the operation (until the end or until an error).
        /// </summary>
        long BytesProcessed { get; }

        /// <summary>
        /// The first error encountered during the process, or <c>null</c> if the process is successful.
        /// </summary>
        Exception Error { get; }
    }
}
