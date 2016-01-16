using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NGitHubdater
{
    /// <summary>
    /// Represents an updatable application.
    /// </summary>
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

        /// <summary>
        /// The name of the application.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The current version of the application implementing the adapter pattern, used for serialization.
        /// </summary>
        public VersionAdapter SerializableVersion { get; set; }

        /// <summary>
        /// The current version of the application.
        /// </summary>
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
