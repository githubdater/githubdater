using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NGitHubdater
{
    [Serializable]
    public class GitHubRepository : IRepository
    {
        private List<string> files;

        public GitHubRepository() { /* for serialization */ }

        public GitHubRepository(string owner, string name, IEnumerable<string> files)
        {
            this.Owner = owner;
            this.Name = name;
            this.files = new List<string>(files);
        }

        public string Domain { get { return "github.com" ; } }
        public string Owner { get; set; }
        public string Name{ get; set; }

        public string ReleaseUrl
        {
            get
            {
                return string.Format("http://{0}/{1}/{2}/releases",
                                      Domain, Owner, Name);
            }
        }

        [XmlIgnore]
        public IEnumerable<string> Files { get { return files; } set { files = new List<string>(value); } }

        public List<string> ReleaseFiles { get { return files; } set { files = value; } }
    }
}
