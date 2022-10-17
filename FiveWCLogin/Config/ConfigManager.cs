namespace FiveWCLogin.Config;

public class ConfigManager : IConfigManager
{
	private readonly IConfiguration _configuration;
	public ConfigManager(IConfiguration configuration) { _configuration = configuration; }
	public int OsuClientId => int.Parse(_configuration["Osu:ClientID"]);
	public string OsuClientSecret => _configuration["Osu:ClientSecret"];
}