using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    /// <summary>
    /// Represents a file published by a release version.
    /// </summary>
    public interface IVersionFile
    {
        /// <summary>
        /// The <b>name</b> of the file (with its extension).
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The size of the file (in bytes).
        /// </summary>
        long Length { get; }

        /// <summary>
        /// The remote Uri of the release file.
        /// </summary>
        Uri Uri { get; }
    }
}
