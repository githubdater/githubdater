using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    [Serializable]
    public class UpdateManifestType : IEquatable<string>
    {
        public static readonly UpdateManifestType GitHub = new UpdateManifestType('h', "github");
        public static readonly UpdateManifestType GitLab = new UpdateManifestType('l', "gitlab");

        public static IEnumerable<UpdateManifestType> Values
        {
            get
            {
                yield return GitHub;
                yield return GitLab;
            }
        }

        private readonly char shortForm;
        private readonly string longForm;

        private UpdateManifestType(char shortForm, string longForm)
        {
            this.shortForm = shortForm;
            this.longForm = longForm;
        }

        public char ShortForm { get { return shortForm; } }
        public string LongForm { get { return longForm; } }

        public static UpdateManifestType Parse(string str)
        {
            if (str == null)
                return null;

            foreach (UpdateManifestType type in Values)
                if (type.Equals(str))
                    return type;

            throw new ArgumentException("Unknown update manifest type '" + str + "'.");
        }

        public override int GetHashCode()
        {
            return (ShortForm + LongForm).GetHashCode();
        }

        public bool Equals(string form)
        {
            if (form == null)
                return false;

            return (form.Equals(ShortForm) || form.Equals(longForm));
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;

            if (obj == null)
                return false;

            if (obj is UpdateManifestType)
                return this.GetHashCode() == obj.GetHashCode();

            return false;
        }

        public override string ToString()
        {
            return LongForm;
        }
    }
}
