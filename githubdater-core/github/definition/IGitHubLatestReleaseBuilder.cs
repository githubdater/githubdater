using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    /// <summary>
    /// Represents a service able to build a <see cref="GitHubRelease"/> from a given URL and a <see cref="GitHubUpdateManifest"/>.
    /// </summary>
    interface IGitHubLatestReleaseBuilder
    {
        /// <summary>
        /// Build a <see cref="GitHubRelease"/> from a given URL and a <see cref="GitHubUpdateManifest"/>.
        /// </summary>
        /// <param name="releasesUrl">The url where the releases are listed.</param>
        /// <param name="manifest">The update manifest used to determine which releases should be retrieved.</param>
        /// <returns></returns>
        GitHubRelease build(string releasesUrl, GitHubUpdateManifest manifest);
    }
}
