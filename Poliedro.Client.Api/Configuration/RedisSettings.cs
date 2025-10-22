namespace Poliedro.Client.Api.Configuration;

public class RedisSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public int CacheExpirationMinutes { get; set; } = 3600;
}
