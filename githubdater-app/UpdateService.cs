using NGitHubdater;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubdater
{
    class UpdateService
    {
        private readonly BackgroundWorker worker;
        private readonly IDictionary<CommandLineParameter, string> providedParameters;

        private readonly UpdateProcess updateProcess;

        public delegate void ProgressChangedHandler(ProgressChangedEventArgs e);
        public event ProgressChangedEventHandler ProgressChangedEvent;

        public UpdateService(BackgroundWorker worker, IDictionary<CommandLineParameter, string> parameters)
        {
            if (worker == null)
                throw new ArgumentNullException("worker");

            this.worker = worker;
            this.updateProcess = new UpdateProcess(parameters);
        }

        public UpdateProcess GetProcess()
        {
            return updateProcess;
        }

        public void Start()
        {
            this.worker.DoWork += DoWork;
            this.worker.RunWorkerAsync();
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            if (worker == null)
                throw new InvalidOperationException("Worker or parameters can't be null.");

            Process initiatingProcess = GetInitiatingProcess(providedParameters);
            string followUpProcessPath = GetParameterValue<string>(CommandLineParameter.FollowUpProcess, providedParameters);

            Task updateTask = updateProcess.Execute((progress) => {
                this.worker.ReportProgress(progress.Percentage, progress);
            });

            updateTask.Wait();
        }

        private static Process GetInitiatingProcess(IDictionary<CommandLineParameter, string> parameters)
        {
            if (parameters == null)
                return null;

            int pid = GetParameterValue<int>(CommandLineParameter.Pid, parameters);

            if (pid <= 0)
                return null;

            return Process.GetProcessById(pid);
        }

        private static T GetParameterValue<T>(CommandLineParameter targetParameter, IDictionary<CommandLineParameter, string> providedParameters)
        {
            if (providedParameters == null)
                return default(T);

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
    }
}
