using System.Diagnostics;

namespace os_query_api.BusinessLogic;

public class Shell : IShell
{
    private const int Timeout = 5000;
    public string Run(string command)
    {
        try
        {
            // Detect current OS to determine the shell to use
            var os = Environment.OSVersion.Platform;
            if (os == PlatformID.Win32NT)
            {
                command = $"cmd /c {command}";
            }
            else
            {
                command = $"bash -c '{command}'";
            }
            
            var psi = new ProcessStartInfo(command)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            var output = process.StandardOutput.ReadToEnd();
            var exited = process.WaitForExit(Timeout);
            if (!exited)
            {
                process?.Kill(true);
                return $"Command execution timed out after {Timeout}ms";
            }
            
            return output ?? string.Empty;
        }
        catch (Exception ex)
        {
            return $"Error executing command: {ex.GetType().Name}";
        }
    }
}