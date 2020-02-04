using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

public class App
{
    public App()
    {
    }

    /// <summary>
    /// 現在のバージョンを取得する
    /// </summary>
    public string GetVersion()
    {
        string filePath = Launcher.Properties.Settings.Default.LocalVersionFilePath;
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath).Trim();
        }

        return "";

    }

    /// <summary>
    /// 最新バージョンを取得する。
    /// </summary>
    public async Task<string> GetLatestVersion(string nowVersion)
    {
        using (HttpClient client = new HttpClient())
        {
            ServicePointManager.SecurityProtocol = 
                SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            try
            {
                string response = await client.GetStringAsync(
                    string.Format(Launcher.Properties.Settings.Default.ServerVersionFilePath, nowVersion));
                return response.Trim();
            }
            catch (Exception)
            {
                return "";
            }
        }

    }
}

