using System;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public class Updater : IUpdater
    {
        private const string DownloadRootDirectoryName = "versions";

        private readonly IUpdateManifest manifest;
        private readonly IUpdateStatusProvider statusProvider;
        private readonly IUpdateDownloader downloader;
        private readonly IUpdateInstaller installer;

        public Updater(IUpdateManifest manifest, 
                       IUpdateStatusProvider statusProvider, 
                       IUpdateDownloader downloader, 
                       IUpdateInstaller installer)
        {
            this.manifest = manifest;
            this.statusProvider = statusProvider;
            this.downloader = downloader;
            this.installer = installer;
        }

        public IUpdateStatus Status()
        {
            return statusProvider.GetStatus(manifest);
        }

        public async Task<DownloadResult> Download(IVersion version, Action<DownloadProgress> callback)
        {
            return await downloader.Download(manifest, version, DownloadRootDirectoryName, (progress) => { callback(progress); });
        }

        public InstallResult Install(IVersion release, Action<IProgress> callback)
        {
            return installer.Install(manifest, release.VersionFiles, DownloadRootDirectoryName, AppDomain.CurrentDomain.BaseDirectory, (progress) => { callback(progress); });
        }
    }
}
