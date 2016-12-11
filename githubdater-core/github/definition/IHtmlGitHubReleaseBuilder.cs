namespace NGitHubdater
{
    /// <summary>
    /// Represents a service able to build a <see cref="GitHubRelease"/> from it's HTML.
    /// </summary>
    interface IHtmlGitHubReleaseBuilder
    {
        /// <summary>
        /// Builds a <see cref="GitHubRelease"/> from it's HTML and an update manifest.
        /// </summary>
        /// <param name="releaseHtml">The release HTML (from it's GitHub page).</param>
        /// <param name="preRelease">Wether or not the release to build is a pre-release. This can't be retrieved from it's inner HTML.</param>
        /// <param name="manifest">The update manifest.</param>
        /// <returns>The <see cref="GitHubRelease"/> parsed from it's GitHub HTML.</returns>
        GitHubRelease build(string releaseHtml, bool preRelease, GitHubUpdateManifest manifest);
    }
}
