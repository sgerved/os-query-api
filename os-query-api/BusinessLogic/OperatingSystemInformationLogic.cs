namespace os_query_api.BusinessLogic;

public class OperatingSystemInformationLogic : IOperatingSystemInformationLogic
{
    public string Ping()
    {
        return "pong";
    }

    public string GetOsVersion()
    {
        return Environment.OSVersion.VersionString;
    }
}