namespace FiveWCLogin.Config;

public class ConfigManager : IConfigManager
{
	private readonly IConfiguration _configuration;
	public ConfigManager(IConfiguration configuration) { _configuration = configuration; }
	public int ClientID => int.Parse(_configuration["Api:ClientID"]);
	public string ClientSecret => _configuration["Api:ClientSecret"];
}