using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NGitHubdater
{
    class SingleZipInstaller : IUpdateInstaller
    {
        private const string NewFileExtension = ".new";

        public string Name { get { return "Single Zip"; } }
        public string ShortHandle { get { return "single-zip"; } }

        public InstallResult Install(IUpdateManifest manifest, IEnumerable<IVersionFile> files, string updateSourceDirectory, string updateTargetDirectory, Action<IProgress> progressCallback)
        {
            if (files == null || files.Count() != 1)
                throw new InvalidOperationException("The update can't contain more than one file.");

            IVersionFile releaseZipFile = files.ElementAt(0);
            string zipPath = Path.Combine(updateSourceDirectory, releaseZipFile.Name);
            string zipDirectory = Path.GetDirectoryName(zipPath);
            long zipSize = 0;

            bool extractingFinished = false;
            bool copyStarted = false;
            bool copyFinished = false;

            FileProcessingProgress progress = null;

            try
            {
                using (var extracting = ZipFile.Read(zipPath))
                {
                    foreach (ZipEntry e in extracting)
                        zipSize += e.UncompressedSize;

                    extracting.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                    extracting.ExtractProgress += (obj, e) =>
                    {
                        if (extractingFinished)
                            return;

                        if (e.BytesTransferred >= e.TotalBytesToTransfer)
                        {
                            string item = e.CurrentEntry == null ? null : e.CurrentEntry.FileName;

                            if (progress == null)
                            {
                                progress = new FileProcessingProgress("2/3 - Extracting... " + item, e.BytesTransferred, zipSize);
                            }
                            else
                            {
                                if (progress.Done)
                                {
                                    item = "Done";
                                    extractingFinished = true;
                                }
                                else if (item != null && item.Contains('/'))
                                {
                                    string[] parts = item.Split('/');

                                    if (parts != null && parts.Count() > 1 && parts[1] != string.Empty)
                                        item = parts.Last();
                                }

                                progress = new FileProcessingProgress("2/3 - Extracting... " + item, progress.BytesProcessed + e.BytesTransferred, zipSize);
                                progressCallback(progress);

                                if (extractingFinished && !copyStarted && !copyFinished)
                                {
                                    string zipEmbeddedRootDirectoryName = extracting.Entries.First().FileName;
                                    string extractedDirectoryPath = Path.Combine(zipDirectory, zipEmbeddedRootDirectoryName);
                                    long directorySize = GetDirectorySize(new DirectoryInfo(extractedDirectoryPath));
                                    long bytesCopied = 0;

                                    Copy(extractedDirectoryPath, AppDomain.CurrentDomain.BaseDirectory, (fileName, copied) =>
                                    {
                                        if (copyFinished)
                                            return;

                                        copyStarted = true;
                                        bytesCopied += copied;

                                        progress = new FileProcessingProgress("3/3 - Copying... " + fileName, bytesCopied, directorySize);

                                        if (progress.Done)
                                        {
                                            copyFinished = true;
                                            progress = new FileProcessingProgress("3/3 - Copying... Done", bytesCopied, directorySize);
                                        }

                                        progressCallback(progress);
                                    });
                                }
                            }
                        }
                    };

                    extracting.ExtractAll(zipDirectory);

                    return new InstallResult(zipSize);
                }
            }
            catch (Exception ex)
            {
                if (progress != null)
                {
                    progressCallback(new FileProcessingProgress("2/3 - Extracting... " + releaseZipFile.Name, progress.BytesProcessed, progress.TotalBytesToProcess, ex));
                    return new InstallResult(progress.BytesProcessed, ex);
                }
                else
                {
                    progressCallback(new FileProcessingProgress("2/3 - Extracting... " + releaseZipFile.Name, 0, 0, ex));
                    return new InstallResult(0, ex);
                }
            }
        }

        private static void Copy(string sourceDirectory, string targetDirectory, Action<string, long> callback)
        {
            Directory.CreateDirectory(targetDirectory);

            foreach (string file in Directory.GetFiles(sourceDirectory))
            {
                string fileName = Path.GetFileName(file);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                string fileExtension = Path.GetExtension(fileName);
                long fileSize = new FileInfo(file).Length;

                if (Utils.UpdateAssemblyNames.Contains(fileName))
                    fileName = fileName + NewFileExtension;

                string newFile = Path.Combine(targetDirectory, fileName);

                File.Copy(file, newFile, true);
                callback(fileName, fileSize);
            }

            string[] directories = Directory.GetDirectories(sourceDirectory);

            foreach (string directory in directories)
            {
                string directoryName = Path.GetFileName(directory);
                string newDirectory = Path.Combine(targetDirectory, directoryName);
                Copy(directory, newDirectory, callback);
            }
        }

        private static long GetDirectorySize(DirectoryInfo rootDirectoryInfo)
        {
            long size = 0;

            FileInfo[] fis = rootDirectoryInfo.GetFiles();

            foreach (FileInfo fi in fis)
                size += fi.Length;

            DirectoryInfo[] dis = rootDirectoryInfo.GetDirectories();

            foreach (DirectoryInfo di in dis)
                size += GetDirectorySize(di);

            return size;
        }
    }
}
