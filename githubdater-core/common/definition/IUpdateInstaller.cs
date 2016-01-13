using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    /// <summary>
    /// Represents a service able to install an update.
    /// </summary>
    public interface IUpdateInstaller
    {
        /// <summary>
        /// The name of the update installer (have to be hard coded most of the time).
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The shorthandle of the update installer (have to be hard coded most of the time).
        /// </summary>
        string ShortHandle { get; }

        /// <summary>
        /// Install an update using the provided arguments.
        /// </summary>
        /// <param name="manifest">The update manifest.</param>
        /// <param name="files">The files to install.</param>
        /// <param name="updateSourceDirectory">The directory where the update files are located (before installation).</param>
        /// <param name="updateTargetDirectory">The directory where the update will be installed.</param>
        /// <param name="progressCallback">The installation progress callback.</param>
        /// <returns>The result of the install process.</returns>
        InstallResult Install(IUpdateManifest manifest, IEnumerable<IVersionFile> files, string updateSourceDirectory, string updateTargetDirectory, Action<IProgress> progressCallback);
    }
}
