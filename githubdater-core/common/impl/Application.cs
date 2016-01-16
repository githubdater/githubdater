using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NGitHubdater
{
    [Serializable]
    public class Application
    {
        private Application() { /* for serialization */ }

        public Application(string name, IVersion version)
        {
            this.Name = name;
            this.Version = version;
            this.SerializableVersion = new VersionAdapter(version);
        }

        public string Name { get; set; }

        public VersionAdapter SerializableVersion { get; set; }

        [XmlIgnore]
        public IVersion Version { get; set; }

        public override int GetHashCode()
        {
            return (Name + Version).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;

            if (obj == null)
                return false;

            if (obj is Application)
                return this.GetHashCode() == obj.GetHashCode();

            return false;
        }

        public override string ToString()
        {
            return Name + " " + Version;
        }
    }
}
