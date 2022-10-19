using OsuSharp.Interfaces;

namespace FiveWCLoginAPI;

public interface IOsuService
{
	Task<IOsuToken> ResolveTokenAsync(string code);
	Task<IGlobalUser> ResolveUserAsync();
}