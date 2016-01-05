using NGitHubdater;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{ 
    public class UpdateProcess
    {
        private const int DefaultKillInitiatingProcessTimeout = 1000;
        private readonly int killInitiatingProcessTimeout;

        private readonly Process initiatingProcess;
        private readonly string followUpProcessPath;
        private readonly IUpdateManifest manifest;
        private readonly string manifestPath;
        private readonly IUpdater updater;

        public UpdateProcess(IDictionary<CommandLineParameter, string> parameters)
            : this(parameters, DefaultKillInitiatingProcessTimeout)
        { }

        public UpdateProcess(IDictionary<CommandLineParameter, string> parameters, int killInitiatingProcessTimeout)
        {
            this.initiatingProcess = GetInitiatingProcess(parameters);

            KeyValuePair<IUpdateManifest, string> manifestAndPath = RetrieveUpdateManifest(parameters);
            this.manifest = manifestAndPath.Key;
            this.manifestPath = manifestAndPath.Value;

            this.updater = UpdaterFactory.Create(manifest);
            this.followUpProcessPath = GetParameterValue<string>(CommandLineParameter.FollowUpProcess, parameters);
            this.killInitiatingProcessTimeout = killInitiatingProcessTimeout;
        }

        public Process GetInitiatingProcess()
        {
            return initiatingProcess;
        }

        public IUpdateManifest GetManifest()
        {
            return manifest;
        }

        public IUpdater GetUpdater()
        {
            return updater;
        }

        public string GetFollowUpProcessPath()
        {
            return followUpProcessPath;
        }

        private static T GetParameterValue<T>(CommandLineParameter targetParameter, IDictionary<CommandLineParameter, string> providedParameters)
        {
            foreach (CommandLineParameter providedParameter in providedParameters.Keys)
            {
                if (providedParameter.Equals(targetParameter))
                {
                    string parameterValue = providedParameters[providedParameter];
                    return (T)Convert.ChangeType(parameterValue, typeof(T));
                }
            }

            return default(T);
        }

        private static KeyValuePair<IUpdateManifest, string> RetrieveUpdateManifest(IDictionary<CommandLineParameter, string> providedParameters)
        {
            string strUpdateManifestType = GetParameterValue<string>(CommandLineParameter.UpdateManifestType, providedParameters);
            UpdateManifestType updateManifestType = UpdateManifestType.Parse(strUpdateManifestType);

            string updateManifestName = GetParameterValue<string>(CommandLineParameter.UpdateManifestName, providedParameters);

            if (updateManifestType != null && updateManifestName != null && updateManifestName != string.Empty)
                return UpdateManifestService.Retrieve(updateManifestType, updateManifestName);
            else
                return UpdateManifestService.Retrieve();
        }

        private static Process GetInitiatingProcess(IDictionary<CommandLineParameter, string> parameters)
        {
            if (parameters == null)
                return null;

            int pid = GetParameterValue<int>(CommandLineParameter.Pid, parameters);

            if (pid <= 0)
                return null;

            try
            {
                return Process.GetProcessById(pid);
            }
            catch (ArgumentException ae)
            {
                Console.Error.Write(ae);
                return null;
            }
            
        }

        public void Execute(Action<IProgress> progressCallback)
        {
            var status = updater.Status();

            if (!status.UpToDate)
            {
                DownloadResult downloadResult = updater.Download(status.LatestVersion, (downloadProgress) =>
                {
                    progressCallback(downloadProgress);
                });

                KillProcess(initiatingProcess);

                InstallResult installResult = updater.Install(status.LatestVersion, (installProcess) =>
                {
                    progressCallback(installProcess);
                });

                if (installResult.IsSuccess)
                {
                    this.manifest.Application.Version = status.LatestVersion;

                    ISerializer serializer = SerializerFactory.GetInstance();
                    MethodInfo serializerGetInstance = serializer.GetType().GetMethod("Save");
                    Type manifestType = manifest.GetType();
                    MethodInfo genericSerializerGetInstance = serializerGetInstance.MakeGenericMethod(manifestType);
                    genericSerializerGetInstance.Invoke(serializer, new object[] { manifestPath, manifest });
                }

                StartProcess(followUpProcessPath);
                Environment.Exit(1);
            }
        }

        private void KillProcess(Process process)
        {
            if (process != null)
            {
                process.Kill();
                bool exit = process.WaitForExit(killInitiatingProcessTimeout);

                if (!exit)
                    throw new InvalidOperationException("The update process can't kill the process : " + process.ProcessName);
            }
        }

        private static void StartProcess(string processPath)
        {
            if (processPath != null && processPath != string.Empty)
                Process.Start(processPath);
        }
    }
}
