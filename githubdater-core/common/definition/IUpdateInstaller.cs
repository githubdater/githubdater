using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public interface IUpdateInstaller
    {
        string Name { get; }
        string ShortHandle { get; }

        InstallResult Install(IUpdateManifest manifest, IEnumerable<IVersionFile> files, string updateSourceDirectory, string updateTargetDirectory, Action<IProgress> progressCallback);
    }
}
