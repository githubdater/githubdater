using System;
using System.Xml.Serialization;

namespace NGitHubdater
{
    [Serializable]
    [XmlType(TypeName = "VersionFile")]
    public class VersionFileAdapter : IVersionFile
    {
        private VersionFileAdapter() { /* for serialization */ }

        public VersionFileAdapter(string name, long length, Uri uri)
        {
            this.Name = name;
            this.Length = length;
            this.Uri = uri;
        }

        public string Name { get; set; } 
        public long Length { get; set; }
        public Uri Uri { get; set; } 
    }
}
