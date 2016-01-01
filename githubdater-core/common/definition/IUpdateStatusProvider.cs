using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public interface IUpdateStatusProvider
    {
        IUpdateStatus GetStatus(IUpdateManifest manifest);
    }
}
