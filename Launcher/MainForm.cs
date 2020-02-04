using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Launcher
{
    public partial class ProgressForm : Form
    {
        public string fromVersion;
        public string toVersion;
        public string appDirectory;
        private string localPath = "";
        private WebClient client = new WebClient();

        public ProgressForm()
        {
            InitializeComponent();
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            this.Text = Properties.Settings.Default.DownloadTitle;

            ServicePointManager.SecurityProtocol = 
                SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            try
            {
                Uri uri = new Uri(
                    string.Format(Properties.Settings.Default.ServerPatchFilePath,
                        fromVersion, toVersion));
                localPath = Path.GetTempPath() +
                    string.Format(Properties.Settings.Default.PatchFilename,
                        fromVersion, toVersion);
                client.DownloadFileCompleted += CompleteDownloadProc;
                client.DownloadProgressChanged += ProgressDownloadProc;
                client.DownloadFileAsync(uri, localPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CompleteDownloadProc(Object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled) return;
            if (e.Error != null)
            {
                MessageBox.Show(this, "ダウンロードに失敗しました。\n" + e.Error.Message, 
                    Properties.Settings.Default.Title, 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            // レジストリにパスを書きこむ
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey(Properties.Settings.Default.RegistryPathKey);
            regKey.SetValue(Properties.Settings.Default.RegistryKey, appDirectory);
            regKey.Close();


            // 自己解凍プログラム起動
            System.Diagnostics.Process.Start(@localPath);

            // 終了
            Application.Exit();

        }

        private void ProgressDownloadProc(Object sener, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            client.CancelAsync();
            this.Close();

        }
    }
}
