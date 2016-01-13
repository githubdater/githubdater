﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    /// <summary>
    /// Represents a service able to download an update (not installing it, see <see cref="IUpdateInstaller"/>).
    /// </summary>
    public interface IUpdateDownloader
    {
        /// <summary>
        /// The name of the update downloader (have to be hard coded most of the time).
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The shorthandle of the update downloader (have to be hard coded most of the time).
        /// </summary>
        string ShortHandle { get; }

        /// <summary>
        /// Download an update using the provided arguments.
        /// </summary>
        /// <param name="manifest">The update manifest.</param>
        /// <param name="targetVersion">The release version to download.</param>
        /// <param name="targetDirectory">The directory where downloaded files will be placed.</param>
        /// <param name="progressCallback">The download progress callback.</param>
        /// <returns>The result of the download process.</returns>
        DownloadResult Download(IUpdateManifest manifest, IVersion targetVersion, string targetDirectory, Action<DownloadProgress> progressCallback);
    }
}
