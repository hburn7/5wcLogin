namespace FiveWCLoginAPI.Config;

public class ConfigManager : IConfigManager
{
	private readonly IConfiguration _configuration;
	public ConfigManager(IConfiguration configuration) { _configuration = configuration; }
	public bool IsDocker => bool.Parse(_configuration["Flags:IsDocker"]);
	public string DbHost => _configuration["Database:Host"];
	public int DbPort => int.Parse(_configuration["Database:Port"]);
	public string DbUsername => _configuration["Database:Username"];
	public string DbPassword => _configuration["Database:Password"];
	public string DbName => _configuration["Database:Database"];
	public int OsuClientId => int.Parse(_configuration["Osu:ClientId"]);
	public string OsuClientSecret => _configuration["Osu:ClientSecret"];

	public string GetConnectionString() =>
		IsDocker
			? $"Server=db;Database={DbName};Port={DbPort};User id={DbUsername};Password={DbPassword}"
			: $"Server={DbHost};Database={DbName};Port={DbPort};User id={DbUsername};Password={DbPassword}";
	
	// public string GetConnectionString() => IsDocker
	// 	? $"postgresql://{DbUsername}:{DbPassword}@db/{DbName}"
	// 	: $"postgresql://{DbUsername}:{DbPassword}@{DbHost}:{DbPort}/{DbName}";
}