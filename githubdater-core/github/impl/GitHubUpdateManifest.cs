using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NGitHubdater
{
    /// <summary>
    /// TODO: Make the GitHubUpdateManifest class immutable without breaking serialization.
    /// </summary>
    [Serializable]
    public class GitHubUpdateManifest : IUpdateManifest
    {
        private const string ApplicationElementName = "Application";
        private const string ApplicationNameAttributeName = "Name";

        private const string ApplicationVersionElementName = "Version";
        private const string ApplicationVersionTagAttributeName = "Tag";
        private const string ApplicationVersionIsPreReleaseAttributeName = "IsPreRelease";

        private const string AllowPreReleaseElementName = "AllowPreRelease";
        private const string InstallTypeElementName = "InstallType";

        private const string NodeCantBeFoundMessage = "The node '{0}' can't be found inside the update manifest.";

        private GitHubUpdateManifest() { /* for serialization */ }

        public GitHubUpdateManifest(Application application, IRepository repository, bool allowPreRelease, InstallType installType)
        {
            this.Application = application;
            this.Repository = repository;
            this.AllowPreRelease = allowPreRelease;
            this.InstallType = installType;
        }

        public Application Application { get; set; }
        public IRepository Repository { get; private set; }
        public bool AllowPreRelease { get; private set; }
        public InstallType InstallType { get; private set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (!reader.ReadToFollowing(ApplicationElementName))
                throw new ArgumentException(string.Format(NodeCantBeFoundMessage, ApplicationElementName));

            string applicationName = reader.GetAttribute(ApplicationNameAttributeName);

            if (!reader.ReadToFollowing(ApplicationVersionElementName))
                throw new ArgumentException(string.Format(NodeCantBeFoundMessage, ApplicationVersionElementName));

            string versionTag = reader.GetAttribute(ApplicationVersionTagAttributeName);
            string strVersionIsPreRelease = reader.GetAttribute(ApplicationVersionIsPreReleaseAttributeName);
            bool versionPreRelease = false;
            Boolean.TryParse(strVersionIsPreRelease, out versionPreRelease);

            string repositoryTypeName = typeof(GitHubRepository).Name;

            if (!reader.ReadToFollowing(repositoryTypeName))
                throw new ArgumentException(string.Format(NodeCantBeFoundMessage, repositoryTypeName));

            XmlSerializer serializer = new XmlSerializer(typeof(GitHubRepository));
            GitHubRepository repository = (GitHubRepository) serializer.Deserialize(reader);

            reader.ReadStartElement(AllowPreReleaseElementName);
            string strAllowPreRelease = reader.ReadContentAsString();
            bool allowPreRelease = false;
            Boolean.TryParse(strVersionIsPreRelease, out allowPreRelease);
            reader.ReadEndElement();

            reader.ReadStartElement(InstallTypeElementName);
            string strInstallType = reader.Value;
            InstallType installType = InstallType.Parse(strInstallType);

            VersionAdapter version = new VersionAdapter(versionTag, versionPreRelease);
            Application application = new Application(applicationName, version);

            this.Application = application;
            this.Repository = repository;
            this.AllowPreRelease = allowPreRelease;
            this.InstallType = installType;
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            writer.WriteStartElement(ApplicationElementName);
            writer.WriteAttributeString(ApplicationNameAttributeName, Application.Name);

            writer.WriteStartElement(ApplicationVersionElementName);
            writer.WriteAttributeString(ApplicationVersionTagAttributeName, Application.Version.Tag);
            writer.WriteAttributeString(ApplicationVersionIsPreReleaseAttributeName, Application.Version.IsPreRelease.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();

            XmlSerializer serializer = new XmlSerializer(typeof(GitHubRepository));
            serializer.Serialize(writer, this.Repository, ns);

            writer.WriteElementString(AllowPreReleaseElementName, AllowPreRelease.ToString());
            writer.WriteElementString(InstallTypeElementName, InstallType == null ? null : InstallType.Code);
        }
    }
}
