using FiveWCLoginAPI.Config;
using OsuSharp;
using OsuSharp.Domain;
using OsuSharp.Interfaces;

namespace FiveWCLoginAPI;

public class OsuService : IOsuService
{
	public IOsuClient Client { get; }
	private const string authUri = "https://5wc.stagec.xyz/";

	public OsuService(IOsuClient client, ConfigManager config)
	{
		Client = client;
		Client.Configuration.ClientId = config.OsuClientId;
		Client.Configuration.ClientSecret = config.OsuClientSecret;
	}

	public async Task<IOsuToken> ResolveTokenAsync(string code) => await Client.GetAccessTokenFromCodeAsync(code, authUri);
	public async Task<IGlobalUser> ResolveUserAsync() => await Client.GetCurrentUserAsync(GameMode.Osu);
}