using System;
using System.Collections.Generic;

namespace NGitHubdater
{
    /// <summary>
    /// Represents a release version.
    /// </summary>
    public interface IVersion : IComparable<IVersion>
    {
        /// <summary>
        /// The tag of the version. Most of the time, it corresponds with a Git tag.
        /// </summary>
        string Tag { get; }

        /// <summary>
        /// The changelog of the release version, or <c>null</c> if there is no changelog.
        /// </summary>
        string ChangeLog { get; }

        /// <summary>
        /// <para>Returns <c>true</c> if the release version is a pre-release, <c>false</c> otherwise.</para>
        /// <para>For example, GitHub let you specify if a release is a pre-release. You could set your own conditions for that.</para>
        /// </summary>
        bool IsPreRelease { get; }

        /// <summary>
        /// Holds the list of files published within the release.
        /// </summary>
        IEnumerable<IVersionFile> VersionFiles { get; }
    }
}
