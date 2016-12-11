namespace NGitHubdater
{
    /// <summary>
    /// Represents a service able to retrieve an update status (up to date or not).
    /// </summary>
    public interface IUpdateStatusProvider
    {
        /// <summary>
        /// Returns the update status with respect to the provided update manifest.
        /// </summary>
        /// <param name="manifest">The update manifest used to determine the update status.</param>
        /// <returns>The update status.</returns>
        IUpdateStatus GetStatus(IUpdateManifest manifest);
    }
}
