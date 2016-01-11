using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    /// <summary>
    /// Represents any sort of progress.
    /// </summary>
    public interface IProgress
    {
        /// <summary>
        /// The title of the progress.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// The current percentage of the progress.
        /// </summary>
        int Percentage { get; }

        /// <summary>
        /// <para>Returns <c>true</c> if the progress is done, <c>false</c> otherwise.</para>
        /// <para>This is mostly a convenience property since its value can be deducted from <see cref="Percentage"/>.
        /// However, it could happen that the progress can be done even though <see cref="Percentage"/> does not hold 100 as a value.</para>
        /// </summary>
        bool Done { get; }

        /// <summary>
        /// Returns <c>true</c> if the progress has encountered an error, <c>false</c> otherwise.
        /// </summary>
        Exception Error { get; }
    }
}
