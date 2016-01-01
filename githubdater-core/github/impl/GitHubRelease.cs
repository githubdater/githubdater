using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    [Serializable]
    public class GitHubRelease : IVersion
    {
        private readonly IRepository repository;
        private readonly string tag;
        private readonly string name;
        private readonly string body;
        private IEnumerable<IVersionFile> releaseFiles;
        private readonly bool preRelease;

        private GitHubRelease() { /* for serialization */ }

        public GitHubRelease(IRepository repository, string tag, string name, string body, IEnumerable<IVersionFile> releaseFiles, bool preRelease)
        {
            this.repository = repository;
            this.tag = tag;
            this.name = name;
            this.body = body;
            this.releaseFiles = releaseFiles;
            this.preRelease = preRelease;
        }

        public IRepository Repository { get { return repository; } }

        public string Name { get { return name; } }
        public string Tag { get { return tag; } }
        public string Body { get { return body; } }
        public bool IsPreRelease { get { return preRelease; } }

        public string ChangeLog { get { return body; } }
        public IEnumerable<IVersionFile> VersionFiles { get { return releaseFiles; } }

        public int CompareTo(IVersion other)
        {
            return this.Tag.CompareTo(other.Tag);
        }
    }
}
