using System.Diagnostics;
using os_query_api.BusinessLogic.Models;

namespace os_query_api.BusinessLogic;

public class Shell : IShell
{
    private const int Timeout = 5000;
    private static readonly HashSet<string> AllowedCommands = ["ls", "pwd", "echo", "whoami"];

    public async Task<string> Run(ExecuteCommandShellModel commandModel)
    {
        try
        {
            // Only allow commands defined in the AllowedCommands and without arguments.
            if (string.IsNullOrWhiteSpace(commandModel.Command) || 
                !AllowedCommands.Contains(commandModel.Command))
            {
                return $"Invalid command: {commandModel.Command}";
            }

            string shell;
            string arguments;
            // Detect current OS to determine the shell to use
            var os = Environment.OSVersion.Platform;
            if (os == PlatformID.Win32NT)
            {
                shell = "cmd";
                arguments = $"/c {commandModel.Command}";
            }
            else
            {
                shell = "bash";
                arguments = $"-c {commandModel.Command}";
            }

            var psi = new ProcessStartInfo(shell)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                ArgumentList = { arguments }
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