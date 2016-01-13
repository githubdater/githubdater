using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NGitHubdater
{
    /// <summary>
    /// <para>Represents an update manifest.</para>
    /// <para>An update manifest is a set of data (mainly properties) defining how to download and install an update.</para>
    /// </summary>
    public interface IUpdateManifest : IXmlSerializable
    {
        /// <summary>
        /// The application to update.
        /// TODO: define an interface for <see cref="ApplicationAdapter"/>.
        /// </summary>
        ApplicationAdapter Application { get; }

        /// <summary>
        /// The repository of the application to update. Most of the time it will be a Git repository but it can be anything else.
        /// </summary>
        IRepository Repository { get; }

        /// <summary>
        /// The install type/method of the application.
        /// </summary>
        InstallType InstallType { get; }

        /// <summary>
        /// Indicate wether pre-releases must be downloaded and installed or not.
        /// </summary>
        bool AllowPreRelease { get; }
    }
}
