using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public class InstallResult : AbstractFileProcessingResult
    {
        public InstallResult(long bytesInstalled) : base(bytesInstalled) { }
        public InstallResult(long bytesInstalled, Exception error) : base(bytesInstalled, error) { }
    }
}
