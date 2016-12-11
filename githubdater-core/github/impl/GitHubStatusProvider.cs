namespace NGitHubdater
{
    class GitHubStatusProvider : IUpdateStatusProvider
    {
        public IUpdateStatus GetStatus(IUpdateManifest manifest)
        {
            GitHubUpdateManifest gitHubUpdateManifest = (GitHubUpdateManifest) manifest;

            string releasesUrl = string.Format("http://github.com/{0}/{1}/releases",
                                                 manifest.Repository.Owner,
                                                 manifest.Repository.Name);

            GitHubRelease latestRelease = new GitHubLatestReleaseHtmlBuilder().build(releasesUrl, gitHubUpdateManifest);
            IVersion currentVersion = manifest.Application.Version;

            return new UpdateStatusAdapter(currentVersion, latestRelease);
        }
    }
}
