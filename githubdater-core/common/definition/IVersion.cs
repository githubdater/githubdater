using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public interface IVersion : IComparable<IVersion>
    {
        string Tag { get; }
        string ChangeLog { get; }

        bool IsPreRelease { get; }
        IEnumerable<IVersionFile> VersionFiles { get; }
    }
}
