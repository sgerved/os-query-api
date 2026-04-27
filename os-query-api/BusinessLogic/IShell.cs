namespace os_query_api.BusinessLogic;

public interface IShell
{
    Task<string> Run(string argument);
}