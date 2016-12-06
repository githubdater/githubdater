using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    /// <summary>
    /// <para>Represents a service able to handle update status retrieving, udate downloading and update installing.
    /// These steps represent the overall process of updating an application.</para>
    /// <para>There seems to be a "problem" with this interface because it depends a lot on the implementation's state.</para>
    /// </summary>
    public interface IUpdater
    {
        /// <summary>
        /// Returns the update status of the application.
        /// </summary>
        /// <returns><c>True</c> if the application is up to date, <c>false</c> otherwise.</returns>
        IUpdateStatus Status();

        /// <summary>
        /// Handle the process of downloading the update.
        /// </summary>
        /// <param name="version">The release version to download.</param>
        /// <param name="callback">The download progress callback.</param>
        /// <returns>The result of the download process.</returns>
        Task<DownloadResult> Download(IVersion version, Action<DownloadProgress> callback);

        /// <summary>
        /// Handle the process of installing the update.
        /// </summary>
        /// <param name="version">The release version to install.</param>
        /// <param name="callback">The install progress callback.</param>
        /// <returns>The result of the install process.</returns>
        InstallResult Install(IVersion version, Action<IProgress> callback);
    }
}
