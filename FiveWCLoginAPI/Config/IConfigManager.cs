namespace FiveWCLoginAPI.Config;

public interface IConfigManager
{
	public string DbHost { get; }
	public int DbPort { get; }
	public string DbUsername { get; }
	public string DbPassword { get; }
	public string DbName { get; }

	public string GetConnectionString();
}