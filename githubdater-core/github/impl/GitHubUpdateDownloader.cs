using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    class GitHubUpdateDownloader : IUpdateDownloader
    {
        public string Name { get { return "GitHub Update Downloader" ;} }
        public string ShortHandle { get { return "GitHub"; } }

        public DownloadResult Download(IUpdateManifest manifest, IVersion targetVersion, string targetDirectory, Action<DownloadProgress> progressCallback)
        {
            if (Directory.Exists(targetDirectory))
                Directory.Delete(targetDirectory, true);

            Directory.CreateDirectory(targetDirectory);

            DownloadProgress progress = null;

            IEnumerable<IVersionFile> releaseFiles = targetVersion.VersionFiles;

            foreach (IVersionFile file in releaseFiles)
            {
                using (var wc = new WebClient())
                {
                    var lockThis = new object();

                    wc.DownloadProgressChanged += (sender, e) =>
                    {
                        lock (lockThis)
                        {
                            progress = new DownloadProgress("Downloading... " + file.Name, e.BytesReceived, e.TotalBytesToReceive);
                            progressCallback(progress);
                        }
                    };

                    wc.DownloadFileCompleted += (sender, e) =>
                    {
                        if (e.Error != null)
                        {
                            if (progress == null)
                                progressCallback(new DownloadProgress("Downloading... " + file.Name, 0, file.Length, e.Error));
                            else
                                progressCallback(new DownloadProgress("Downloading... " + file.Name, progress.BytesProcessed, progress.TotalBytesToProcess, e.Error));
                        }
                        else
                        {
                            progress = new DownloadProgress("Downloading... Done", progress.TotalBytesToProcess, progress.TotalBytesToProcess);
                            progressCallback(progress);
                        }
                    };

                    string localPath = Path.Combine(targetDirectory, Path.GetFileName(file.Uri.ToString()));
                    wc.DownloadFileTaskAsync(file.Uri, localPath).Wait();
                }
            }

            if (progress.Error == null)
                return new DownloadResult(progress.BytesProcessed);
            else
                return new DownloadResult(progress.BytesProcessed, progress.Error);
        }
    }
}
