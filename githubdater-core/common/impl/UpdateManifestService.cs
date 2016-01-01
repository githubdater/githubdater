using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NGitHubdater
{
    public class UpdateManifestService
    {
        private const string GitHubUpdateManifestFileName = "github.update.manifest";
        private const string GitLabUpdateManifestFileName = "gitlab.update.manifest";

        private static readonly string GitHubUpdateManifestPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GitHubUpdateManifestFileName);
        private static readonly string GitLabUpdateManifestPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GitHubUpdateManifestFileName);

        public static KeyValuePair<IUpdateManifest, string> Retrieve()
        {
            if (File.Exists(GitHubUpdateManifestPath))
                return Retrieve(UpdateManifestType.GitHub, GitHubUpdateManifestPath);
            else if (File.Exists(GitLabUpdateManifestPath))
            {
                //TODO
                throw new NotSupportedException("GitLab manifest is not yet handled.");
            }
            else
            {
                throw new InvalidOperationException("Can't find an update manifest at the root of the application.");
            }
        }

        public static  KeyValuePair<IUpdateManifest, string> Retrieve(UpdateManifestType manifestType, string manifestPath)
        {
            IUpdateManifest manifest = null;

            if (manifestPath == null || manifestPath == string.Empty)
                manifestPath = GitHubUpdateManifestFileName;

            if (manifestType == UpdateManifestType.GitHub)
                manifest = SerializerFactory.GetInstance().Load<GitHubUpdateManifest>(manifestPath);
            else
                throw new ArgumentException("Manifest type '" + manifestType + "' is not yet handled.");

            return new KeyValuePair<IUpdateManifest, string>(manifest, manifestPath);
        }

    }
}
