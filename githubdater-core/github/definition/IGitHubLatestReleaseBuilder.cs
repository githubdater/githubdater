using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    interface IGitHubLatestReleaseBuilder
    {
        GitHubRelease build(string releasesUrl, GitHubUpdateManifest manifest);
    }
}
