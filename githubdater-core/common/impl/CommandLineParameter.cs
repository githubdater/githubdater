using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public class CommandLineParameter : IComparable<CommandLineParameter>, IEquatable<string>
    {
        public static readonly CommandLineParameter Pid = new CommandLineParameter('p', "initiating-process-pid");
        public static readonly CommandLineParameter FollowUpProcess = new CommandLineParameter('f', "follow-up-process-path");

        public static readonly CommandLineParameter UpdateManifestName = new CommandLineParameter('u', "update-manifest-name");
        public static readonly CommandLineParameter UpdateManifestType = new CommandLineParameter('t', "update-manifest-type");

        /*public static readonly CommandLineParameter UpdateManifestOwner = new CommandLineParameter('o', "update-manifest-owner");
        public static readonly CommandLineParameter UpdateManifestRepository = new CommandLineParameter('r', "update-manifest-repository");
        public static readonly CommandLineParameter UpdateManifestFiles = new CommandLineParameter('f', "update-manifest-files");
        public static readonly CommandLineParameter UpdateManifestInstallType = new CommandLineParameter('i', "update-manifest-install-type");*/

        public static IEnumerable<CommandLineParameter> KnownParameters
        {
            get
            {
                yield return Pid;
                yield return FollowUpProcess;
                yield return UpdateManifestName;
                yield return UpdateManifestType;
                /*yield return UpdateManifestOwner;
                yield return UpdateManifestRepository;
                yield return UpdateManifestFiles;
                yield return UpdateManifestInstallType;*/
            }
        }

        public static IDictionary<CommandLineParameter, string> Parse(string[] args)
        {
            IDictionary<CommandLineParameter, string> providedParams = new Dictionary<CommandLineParameter, string>();

            foreach (string arg in args)
            {
                string[] keyValue = arg.Split('=');

                if (keyValue == null || keyValue.Length != 2)
                    continue;

                string key = keyValue[0];
                string value = keyValue[1];

                if (key == null)
                    throw new ArgumentException("Null parameter.");

                if (value == null)
                    throw new ArgumentException("Null value.");

                bool parameterIsKnown = false;

                foreach (CommandLineParameter knownParameter in CommandLineParameter.KnownParameters)
                {
                    if (knownParameter.Equals(key))
                    {
                        parameterIsKnown = true;
                        providedParams.Add(knownParameter, value);
                        break;
                    }
                }

                if (!parameterIsKnown)
                    throw new ArgumentException("Unkown parameter '" + key + "'.");
            }

            return providedParams;
        }

        private readonly char shortForm;
        private readonly string longForm;
        private readonly string description;

        private CommandLineParameter(char shortForm, string longForm) : this(shortForm, longForm, null) { }

        private CommandLineParameter(char shortForm, string longForm, string description)
        {
            this.shortForm = shortForm;
            this.longForm = longForm;
            this.description = description;
        }

        public char ShortForm { get { return shortForm; } }
        public string LongForm { get { return longForm; } }
        public string Description { get { return description; } }

        public override int GetHashCode()
        {
            return (ShortForm + LongForm).GetHashCode();
        }

        public bool Equals(string str)
        {
            if (str == null)
                return false;

            if (str.StartsWith("-"))
                str = str.Substring(1);

            return (ShortForm.ToString().Equals(str) || LongForm.Equals(str));
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;

            if (obj == null)
                return false;

            if (obj is CommandLineParameter)
                return this.GetHashCode() == obj.GetHashCode();

            return false;
        }

        public override string ToString()
        {
            return "-" + LongForm + ", --" + LongForm + (description == null ? string.Empty : "\t" + Description);
        }

        public int CompareTo(CommandLineParameter other)
        {
            return this.ToString().CompareTo(other.ToString());
        }
    }
}
