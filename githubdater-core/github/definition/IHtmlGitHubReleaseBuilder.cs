using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    interface IHtmlGitHubReleaseBuilder
    {
        GitHubRelease build(string releaseHtml, bool preRelease, GitHubUpdateManifest manifest);
    }
}
