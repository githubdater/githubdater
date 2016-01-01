using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public interface IRepository
    {
        string Domain { get; }
        string Owner { get; }
        string Name { get; }

        string ReleaseUrl { get; }

        IEnumerable<string> Files { get; }
    }
}
