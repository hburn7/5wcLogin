namespace FiveWCLogin.Config;

public interface IConfigManager
{
	public int OsuClientId { get; }
	public string OsuClientSecret { get; }
}