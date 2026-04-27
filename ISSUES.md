# Known Issues

Remaining issues identified during code analysis. Ordered by priority.

---

## Medium

### No logging or audit trail
No `ILogger<T>` is injected anywhere in the project. This is especially important for
`Shell.cs` — there is currently no record of which commands were executed, by whom,
or whether they succeeded. This makes debugging and security auditing impossible.

**Affected files:** `BusinessLogic/Shell.cs`, `BusinessLogic/OperatingSystemInformationLogic.cs`

---

## Low

### Timeout not configurable
The shell command timeout is hardcoded as a constant and cannot be changed without
recompiling the project.

**Affected file:** `BusinessLogic/Shell.cs:7`
```csharp
private const int Timeout = 5000;
```
**Suggested fix:** Move to `appsettings.json` under a `Shell` section and inject via `IConfiguration`.

---

### AllowedCommands not configurable
The shell command allowlist is hardcoded and requires a recompile to change.

**Affected file:** `BusinessLogic/Shell.cs:8`
```csharp
private static readonly HashSet<string> AllowedCommands = ["ls", "pwd", "echo", "whoami"];
```
**Suggested fix:** Move to `appsettings.json` alongside the timeout setting.

---

### Unused model class
`ExecuteCommandShellModel` is defined but never referenced anywhere in the project.

**Affected file:** `BusinessLogic/Models/ExecuteCommandShellModel.cs`

**Suggested fix:** Delete the file, or use it as the request body for the shell endpoint
(which would also address passing the command via URL).

---

### Controller class name typo
`OperationSystemInformationApiController` should be `OperatingSystemInformationApiController`.
"Operation" is incorrect — the intent is "Operating" (as in Operating System).

**Affected file:** `ApiController/OperationSystemInformationApiController.cs:9`

---

### No tests
There is no test project in the solution. The business logic layer — particularly
`Shell.cs` — has branching logic (OS detection, timeout handling, allowlist validation)
that would benefit from unit test coverage.

**Suggested fix:** Add an xUnit project targeting `BusinessLogic/` with mocked `IShell`
and `IOperatingSystemInformationLogic` dependencies.
