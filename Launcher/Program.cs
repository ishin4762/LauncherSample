using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Launcher
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Assembly myAssembly = Assembly.GetEntryAssembly();
            string appDirectory = System.IO.Path.GetDirectoryName(myAssembly.Location);
            Directory.SetCurrentDirectory(appDirectory);


            App app = new App();

            // 現在のバージョンを取得
            string version = app.GetVersion();
            bool execApp = true;

            if (version != "")
            {
                // サーバの最新バージョンを取得
                Task<string> getVersionTask = app.GetLatestVersion(version);
                getVersionTask.Wait();
                string latestVersion = getVersionTask.Result;

                if (latestVersion != "" && latestVersion != version)
                {
                    // パッチの存在確認
                    DialogResult result = MessageBox.Show(
                        null,
                        string.Format("最新のバージョン：{0}が見つかりました。\nアップデートしますか？\n現在バージョン：{1}",
                            latestVersion, version),
                        Properties.Settings.Default.Title,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        execApp = false;

                        // パッチダウンロード
                        ProgressForm form = new ProgressForm();
                        form.fromVersion = version;
                        form.toVersion = latestVersion;
                        form.appDirectory = appDirectory;
                        Application.Run(form);
                    }
                }
            }

            // 普通にAppを起動
            if (execApp)
            {
                System.Diagnostics.Process.Start(Properties.Settings.Default.ExecuteFilePath);
            }

            // 終了
            Application.Exit();
        }


    }
}
