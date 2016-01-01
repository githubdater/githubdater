using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NGitHubdater
{
    public interface IUpdateManifest : IXmlSerializable
    {
        ApplicationAdapter Application { get; }
        IRepository Repository { get; }
        InstallType InstallType { get; }

        bool AllowPreRelease { get; }
    }
}
