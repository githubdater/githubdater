namespace NGitHubdater
{
    public static class UpdaterFactory
    {
        public static IUpdater Create(IUpdateManifest manifest)
        {
            //TODO: algorithm to instanciate the right types (using the manifest and maybe application args)
            return new Updater(manifest, new GitHubStatusProvider(), new GitHubUpdateDownloader(), new SingleZipInstaller());
        }
    }
}
