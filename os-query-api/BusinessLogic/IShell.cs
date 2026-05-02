using os_query_api.BusinessLogic.Models;

namespace os_query_api.BusinessLogic;

public interface IShell
{
    Task<string> Run(ExecuteCommandShellModel commandModel);
}