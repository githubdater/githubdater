using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public interface IUpdateDownloader
    {
        string Name { get; }
        string ShortHandle { get; }

        DownloadResult Download(IUpdateManifest manifest, IVersion targetVersion, string targetDirectory, Action<DownloadProgress> progressCallback);
    }
}
