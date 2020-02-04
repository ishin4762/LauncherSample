using System;

public class App
{
    public App()
    {
    }

    public double GetVersion()
    {
        string filePath = @"App/version.txt";
        if (File.Exists(filePath))
        {
            string contents = File.ReadAllText(filePath).Substring(0, 4);
            double version;
            if (double.TryParse(contents, out version))
            {
                return version;
            }
        }

        return 0.0;

    }

    public double GetLatestVersion()
    {
        using (HttpClient client = new HttpClient())
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            try
            {
                var res = await client.GetAsync("https://www.brokendesk.jp/pia/latestversion.txt");
                double version;
                if (double.TryParse(response.Substring(0, 4), out version))
                {
                    return version;
                }
            }
            catch (Exception e)
            {
                return 0.0;
            }
        }

        return 0.0;

    }
}

