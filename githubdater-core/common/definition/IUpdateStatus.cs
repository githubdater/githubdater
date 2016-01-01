using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public interface IUpdateStatus
    {
        bool UpToDate { get; }
        IVersion CurrentVersion { get; }
        IVersion LatestVersion { get; }
    }
}
