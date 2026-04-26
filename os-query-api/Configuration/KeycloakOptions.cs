namespace os_query_api.Configuration;

public sealed class KeycloakOptions
{
    public string Url { get; set; } = "";
    public string ClientId { get; set; } = "";
    public string ClientSecret { get; set; } = "";
    public string Realm { get; set; } = "";
    public string Audience { get; set; } = "";
    public string RoleClaimType { get; set; } = "";
    public bool RequireHttpsMetadata { get; set; } = true;
}