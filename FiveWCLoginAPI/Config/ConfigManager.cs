namespace FiveWCLoginAPI.Config;

public class ConfigManager : IConfigManager
{
	private readonly IConfiguration _configuration;
	public ConfigManager(IConfiguration configuration) { _configuration = configuration; }
	public string DbHost => IsDocker ? "host.docker.internal" : _configuration["Database:Host"];
	public int DbPort => int.Parse(_configuration["Database:Port"]);
	public string DbUsername => _configuration["Database:Username"];
	public string DbPassword => _configuration["Database:Password"];
	public string DbName => _configuration["Database:Database"];
	public bool IsDocker => bool.Parse(_configuration["Flags:IsDocker"]);
	public string GetConnectionString() => $"Host={DbHost}:{DbPort};Username={DbUsername};Password={DbPassword};Database={DbName}";
}