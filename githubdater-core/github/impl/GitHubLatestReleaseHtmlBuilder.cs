using HtmlAgilityPack;

namespace NGitHubdater
{
    class GitHubLatestReleaseHtmlBuilder : IGitHubLatestReleaseBuilder
    {
        private const string XpathLatestRelease = "//div[@class='release label-latest']";
        private const string XpathLatestPreRelease = "//div[@class='release label-prerelease']";

        public GitHubRelease build(string releasesUrl, GitHubUpdateManifest manifest)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(releasesUrl);

            HtmlNode nodeLatestRelease = null;
            bool prerelease = false;

            if (manifest.AllowPreRelease)
            {
                nodeLatestRelease = doc.DocumentNode.SelectSingleNode(XpathLatestPreRelease);

                if (nodeLatestRelease != null) // if the latest release is not a pre-release
                    prerelease = true;
                else
                    nodeLatestRelease = doc.DocumentNode.SelectSingleNode(XpathLatestRelease);
            }
            else
            {
                nodeLatestRelease = doc.DocumentNode.SelectSingleNode(XpathLatestRelease);
            }

            return new XpathGitHubReleaseBuilder().build(nodeLatestRelease.InnerHtml, prerelease, manifest);
        }
    }
}
