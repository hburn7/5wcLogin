namespace FiveWCLogin.Config;

public interface IConfigManager
{
	public int ClientID { get; }
	public string ClientSecret { get; }
}