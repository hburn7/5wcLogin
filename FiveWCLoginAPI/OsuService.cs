using FiveWCLoginAPI.Config;
using OsuSharp;
using OsuSharp.Domain;
using OsuSharp.Interfaces;

namespace FiveWCLoginAPI;

public class OsuService : IOsuService
{
	private readonly IOsuClient _client;
	private const string authUri = "https://auth.stagec.xyz/api/osuauth";

	public OsuService(IOsuClient client, ConfigManager config)
	{
		_client = client;
		_client.Configuration.ClientId = config.OsuClientId;
		_client.Configuration.ClientSecret = config.OsuClientSecret;
	}

	public async Task<IOsuToken> ResolveTokenAsync(string code) => await _client.GetAccessTokenFromCodeAsync(code, authUri);
	public async Task<IGlobalUser> ResolveUserAsync() => await _client.GetCurrentUserAsync(GameMode.Osu);
}