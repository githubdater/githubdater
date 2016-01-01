using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    class LocalFile : IVersionFile
    {
        private readonly Uri uri;
        private readonly long length;

        public LocalFile(string path, long length)
        {
            this.uri = new Uri(path);
            this.length = length;
        }

        public string Name { get { return Path.GetFileName(uri.LocalPath); } }
        public long Length { get { return length; } }
        public Uri Uri { get { return uri; } }
    }
}
