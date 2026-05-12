using System.Collections.Concurrent;

namespace os_query_api.BusinessLogic;

public class TokenCacheSingleton : ITokenCacheSingleton
{
    private ConcurrentDictionary<string, string> _cache = new();
    
    public TokenCacheSingleton()
    {
    }
    
    public string GetToken(string key)
    {
        return _cache.TryGetValue(key, out var value) ? value : $"No item in cache : {key}";
    }
    
    public void SetToken(string key, string value)
    {
        _cache[key] = value;
    }
}