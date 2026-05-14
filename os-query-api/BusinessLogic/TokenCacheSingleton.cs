using System.Collections.Concurrent;

namespace os_query_api.BusinessLogic;

public class TokenCacheSingleton(IHttpClientFactory httpClientFactory) : ITokenCacheSingleton
{
    private ConcurrentDictionary<string, string> _cache = new();
    
    public string GetToken(string key)
    {
        return _cache.TryGetValue(key, out var value) ? value : $"No item in cache : {key}";
    }
    
    public void SetToken(string key, string value)
    {
        _cache[key] = value;
    }
    
    private void FetchToken()
    {
        var httpClient = httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri("http://keycloak");
        var response = httpClient.GetAsync("token").Result;
        var token = response.Content.ReadAsStringAsync().Result;
        
        SetToken("access_token", token);
    }
}