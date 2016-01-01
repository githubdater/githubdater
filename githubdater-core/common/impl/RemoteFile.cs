using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    class RemoteFile : IVersionFile
    {
        private readonly string title;
        private readonly long length;
        private readonly Uri uri;

        public RemoteFile(string title, long size, string uri) : this(title, size, new Uri(uri)) { }

        public RemoteFile(string title, long length, Uri uri)
        {
            this.title = title;
            this.length = length;
            this.uri = uri;
        }

        public string Name { get { return title; } }
        public long Length { get { return length; } }
        public Uri Uri { get { return uri; } }

        public override string ToString()
        {
            return title + " (" + length + "Mb)";
        }
    }
}
