namespace NGitHubdater
{
    /// <summary>
    /// Represents an update status (up to date or not).
    /// </summary>
    public interface IUpdateStatus
    {
        /// <summary>
        /// <c>True</c> if the status is up to date, <c>false</c> other wise.
        /// </summary>
        bool UpToDate { get; }

        /// <summary>
        /// The current version used to determine the update status.
        /// </summary>
        IVersion CurrentVersion { get; }

        /// <summary>
        /// The latest version used to determine the update status.
        /// </summary>
        IVersion LatestVersion { get; }
    }
}
