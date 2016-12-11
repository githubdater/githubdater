using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NGitHubdater
{
    [Serializable]
    [XmlType(TypeName = "Version")]
    public class VersionAdapter : IVersion
    {
        public VersionAdapter() { /* for serialization */ }

        public VersionAdapter(IVersion version) 
            : this(version.Tag, version.ChangeLog, version.IsPreRelease, version.VersionFiles) { }

        public VersionAdapter(string tag, bool isPreRelease)
            : this(tag, null, isPreRelease, null) { }

        public VersionAdapter(string tag, string changelog, bool isPreRelease, IEnumerable<IVersionFile> files)
        {
            this.Tag = tag;
            this.ChangeLog = changelog;
            this.IsPreRelease = IsPreRelease;

            if (files != null)
                this.VersionFiles = new List<IVersionFile>(files);
        }

        public string Tag { get; set; }
        public string ChangeLog { get; set; }
        public bool IsPreRelease { get; set; }

        [XmlIgnore]
        public IEnumerable<IVersionFile> VersionFiles { get; set; }

        public int CompareTo(IVersion other)
        {
            return this.Tag.CompareTo(other.Tag);
        }
    }
}
