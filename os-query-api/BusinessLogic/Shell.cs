using System.Diagnostics;

namespace os_query_api.BusinessLogic;

public class Shell : IShell
{
    public string Run(string command)
    {
        var psi = new ProcessStartInfo(command)
        {
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };
        
        try
        {
            var process = Process.Start(psi);
            var outputStd = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return outputStd;
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }
}