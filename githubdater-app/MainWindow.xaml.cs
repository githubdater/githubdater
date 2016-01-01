using NGitHubdater;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GitHubdater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            progressBar.IsIndeterminate = true;

            BackgroundWorker worker = new BackgroundWorker();

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_WorkCompleted;

            IDictionary<CommandLineParameter, string> parameters = CommandLineParameter.Parse(Environment.GetCommandLineArgs());

            if (parameters != null)
                foreach (KeyValuePair<CommandLineParameter, string> kp in parameters)
                    MessageBox.Show(kp.Key + "=" + kp.Value);

            UpdateService updateService = new UpdateService(worker, parameters);

            IUpdateManifest manifest = updateService.GetProcess().GetManifest();
            IUpdateStatus status = updateService.GetProcess().GetUpdater().Status();

            lblTitle.Content = manifest.Application.Name + " " + status.LatestVersion.Tag;

            updateService.Start();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.IsIndeterminate = false;

            IProgress progress = (IProgress)e.UserState;

            if (progress.Error != null)
                throw progress.Error;

            lblStep.Content = progress.Title;

            lblPercentage.Content = progress.Percentage + "%";
            progressBar.Value = progress.Percentage;
        }

        private void Worker_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // do nothing
        }
    }
}
