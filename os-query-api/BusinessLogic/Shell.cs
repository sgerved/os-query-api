using System.Diagnostics;

namespace os_query_api.BusinessLogic;

public class Shell : IShell
{
    public string Run(string command)
    {
        var process = Process.Start(command);
        try
        {
            process.WaitForExit();
            return process.StandardOutput.ReadToEnd();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}