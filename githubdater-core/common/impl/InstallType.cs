using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    /// <summary>
    /// <para>Represents an install type for an updtadable application.</para>
    /// <para>An install type is a process that takes downloaded files and copy them where they
    /// need to be. It can also involve file extraction or any other file processing method.</para>
    /// </summary>
    [Serializable]
    public class InstallType : IEquatable<string>
    {
        public static readonly InstallType SingleZip = new InstallType("single-zip");

        public static IEnumerable<InstallType> Values
        {
            get
            {
                yield return SingleZip;
            }
        }

        private InstallType() { /* for serialization */ }

        private InstallType(string code)
        {
            this.Code = code;
        }

        public string Code { get; set; }

        public static InstallType Parse(string code)
        {
            foreach (InstallType type in Values)
                if (type.Code == code)
                    return type;

            return null;
            //throw new ArgumentException("Unknown install type '" + code + "'.");
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        public bool Equals(string str)
        {
            return (this.Code == str);
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;

            if (obj == null)
                return false;

            if (obj is InstallType)
                return this.GetHashCode() == obj.GetHashCode();

            return false;
        }

        public override string ToString()
        {
            return Code;
        }
    }
}
