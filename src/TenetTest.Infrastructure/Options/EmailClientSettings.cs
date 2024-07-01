namespace TenetTest.Infrastructure.Options;

public class EmailClientSettings
{
    public string HostAddress { get; set; } = string.Empty;
    public string HostPort { get; set; } = string.Empty;
    public string HostUsername { get; set; } = string.Empty;
    public string HostPassword { get; set; } = string.Empty;
    public string HostEmail { get; set; } = string.Empty;
}