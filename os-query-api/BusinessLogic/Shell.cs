using System.Diagnostics;

namespace os_query_api.BusinessLogic;

public class Shell : IShell
{
    private const int Timeout = 5000;
    private static readonly List<string> allowedCommands = new List<string> { "ls", "pwd", "echo", "whoami" };

    public async Task<string> Run(string argument)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(argument) || !allowedCommands.Contains(argument))
            {
                return $"Invalid command: {argument}";
            }

            string command;
            // Detect current OS to determine the shell to use
            var os = Environment.OSVersion.Platform;
            if (os == PlatformID.Win32NT)
            {
                command = $"cmd";
                argument = $"/c {argument}";
            }
            else
            {
                command = $"bash";
                argument = $"-c {argument}";
            }

            var psi = new ProcessStartInfo(command)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                ArgumentList = { argument }
            };

            using var process = Process.Start(psi);
            if (process == null)
                return "Process not started";

            var outputTask = process.StandardOutput.ReadToEndAsync();
            var errorTask = process.StandardError.ReadToEndAsync();

            var exited = process.WaitForExit(Timeout);

            if (!exited)
            {
                process.Kill(entireProcessTree: true);
                return $"Command execution timed out after {Timeout}ms";
            }

            var output = await outputTask;
            var error = await errorTask;

            return !string.IsNullOrWhiteSpace(error) ? error : output;
        }
        catch (Exception ex)
        {
            return $"Error executing command: {ex.GetType().Name}";
        }
    }
}