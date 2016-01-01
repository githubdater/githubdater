using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public interface IProgress
    {
        string Title { get; }
        int Percentage { get; }
        bool Done { get; }

        Exception Error { get; }
    }
}
