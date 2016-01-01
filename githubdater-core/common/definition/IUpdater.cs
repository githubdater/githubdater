using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public interface IUpdater
    {
        IUpdateStatus Status();
        DownloadResult Download(IVersion version, Action<DownloadProgress> callback);
        InstallResult Install(IVersion version, Action<IProgress> callback);
    }
}
