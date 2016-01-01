using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    class XpathGitHubReleaseBuilder : IHtmlGitHubReleaseBuilder
    {
        public GitHubRelease build(string releaseHtml, bool preRelease, GitHubUpdateManifest manifest)
        {
            HtmlDocument releaseDocument = new HtmlDocument();
            releaseDocument.LoadHtml(releaseHtml);

            HtmlNode releaseNode = releaseDocument.DocumentNode;

            string tag = releaseNode.SelectSingleNode("//span[@class='css-truncate-target']").InnerText;
            string name = releaseNode.SelectSingleNode("//h1[@class='release-title']//a").InnerText;
            string htmlBody = releaseNode.SelectSingleNode("//div[@class='markdown-body']").InnerHtml;

            HtmlNodeCollection binariesNodes = releaseNode.SelectNodes("//ul[@class='release-downloads']//a[@href]");

            List<IVersionFile> releaseFiles = new List<IVersionFile>();

            foreach(string file in manifest.Repository.Files)
            {
                string theoricFileName = file.Replace("{version}", tag);

                foreach (HtmlNode node in binariesNodes)
                {
                    string href = node.GetAttributeValue("href", null);

                    if (href.Contains(theoricFileName))
                    {
                        //TODO: clean up this hard coded url
                        string binaryDownloadUrl = "https://github.com" + href;
                        string binaryName = binaryDownloadUrl.Split('/').Last();
                        long binarySize = 0;

                        WebClient wc = new WebClient();
                        wc.OpenRead(binaryDownloadUrl);
                        binarySize = long.Parse(wc.ResponseHeaders["Content-Length"]);

                        releaseFiles.Add(new RemoteFile(binaryName, binarySize, binaryDownloadUrl));
                    }
                }
            }

            return new GitHubRelease(manifest.Repository, tag, name, htmlBody, releaseFiles, preRelease);
        }
    }
}
