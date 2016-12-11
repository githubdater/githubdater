using System.Collections.Generic;

namespace NGitHubdater
{
    /// <summary>
    /// Represents a Git repository.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// The domain name on which the Git repository is hosted (e.g github.com)
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// <para>The owner of the Git repository.</para>
        /// <list type="bullet">Most of the time, this value is attached to the repository "full" name :
        ///   <item>
        ///     <description>
        ///       <b>torvalds</b>/linux
        ///     </description>
        ///   </item>
        ///   <item>
        ///     <description>
        ///       <b>facebook</b>/react
        ///     </description>
        ///   </item>
        /// </list>
        /// </summary>
        string Owner { get; }

        /// <summary>
        /// The name of the Git repository (without its owner).
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The full URL where releases can be found for this Git repository.
        /// </summary>
        string ReleaseUrl { get; }

        /// <summary>
        /// <para>The file names to download from a release.</para>
        /// <para>You can use variable definitions inside these names, such as <c>{version}</c>.
        /// These definitions are useful if the release file names contains variables which values depend on the release version
        /// (e.g. <c>my-application-v1.2.0.zip</c> which you can represent this way : <c>my-application-{version}.zip</c></para>
        /// </summary>
        IEnumerable<string> Files { get; }
    }
}
