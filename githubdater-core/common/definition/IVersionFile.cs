using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    /// <summary>
    /// Represents a file to handle during the update process (download and install)
    /// </summary>
    public interface IVersionFile
    {
        /// <summary>
        /// The name of the file with its extension (without its full path)
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The size of the file in bytes
        /// </summary>
        long Length { get; }

        /// <summary>
        /// The remote Uri of the release file
        /// </summary>
        Uri Uri { get; }
    }
}
