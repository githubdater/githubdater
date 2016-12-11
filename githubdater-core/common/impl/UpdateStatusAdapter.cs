namespace NGitHubdater
{
    class UpdateStatusAdapter : IUpdateStatus
    {
        private readonly IVersion currentVersion;
        private readonly IVersion latestVersion;

        public UpdateStatusAdapter(IVersion currentVersion, IVersion latestVersion)
        {
            this.currentVersion = currentVersion;
            this.latestVersion = latestVersion;
        }

        public IVersion CurrentVersion { get { return currentVersion; } }
        public IVersion LatestVersion { get { return latestVersion; } }

        public bool UpToDate
        {
            get
            {
                if (currentVersion == null)
                    return false;

                return (currentVersion.CompareTo(LatestVersion) >= 0);
            }
        }
    }
}
