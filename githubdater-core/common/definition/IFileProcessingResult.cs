using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public interface IFileProcessingResult
    {
        long BytesProcessed { get; }
        Exception Error { get; }
    }
}
