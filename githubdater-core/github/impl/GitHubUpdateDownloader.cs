using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace NGitHubdater
{
    class GitHubUpdateDownloader : IUpdateDownloader
    {
        public string Name { get { return "GitHub Update Downloader" ;} }
        public string ShortHandle { get { return "GitHub"; } }

        public async Task<DownloadResult> Download(IUpdateManifest manifest, IVersion targetVersion, string targetDirectoryName, Action<DownloadProgress> progressCallback)
        {
            string applicationRoot = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string targetDirectory = Path.Combine(applicationRoot, targetDirectoryName);

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
                            progress = new DownloadProgress(file.Name, e.BytesReceived, e.TotalBytesToReceive);
                            progressCallback(progress);
                        }
                    };

                    wc.DownloadFileCompleted += (sender, e) =>
                    {
                        if (e.Error != null)
                        {
                            if (progress == null)
                                progressCallback(new DownloadProgress(file.Name, 0, file.Length, e.Error));
                            else
                                progressCallback(new DownloadProgress(file.Name, progress.BytesProcessed, progress.TotalBytesToProcess, e.Error));
                        }
                        else
                        {
                            progress = new DownloadProgress("Done", progress.TotalBytesToProcess, progress.TotalBytesToProcess);
                            progressCallback(progress);
                        }
                    };

                    string downloadedFilePath = Path.Combine(targetDirectory, Path.GetFileName(file.Uri.ToString()));

                    await wc.DownloadFileTaskAsync(file.Uri, downloadedFilePath);
                }
            }
            

            if (progress.Error == null)
                return new DownloadResult(progress.BytesProcessed);
            else
                return new DownloadResult(progress.BytesProcessed, progress.Error);
        }
    }
}
