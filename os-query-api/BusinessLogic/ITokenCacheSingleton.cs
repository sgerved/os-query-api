namespace os_query_api.BusinessLogic;

public interface ITokenCacheSingleton
{
    public string GetToken(string key);
    public void SetToken(string key, string value);
}