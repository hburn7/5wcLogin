using FiveWCLoginAPI.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FiveWCLoginAPI.Controllers;

[ApiController]
[Route("api")]
public class OsuUserController : ControllerBase
{
	private const string BaseUrl = "https://osu.ppy.sh/api/v2/";
	private const string TokenUrl = "https://osu.ppy.sh/oauth/token";
	
	private readonly ILogger<OsuUserController> _logger;
	private readonly FiveWCDbContext _dbContext;
	private readonly ConfigManager _config;

	public OsuUserController(ILogger<OsuUserController> logger, FiveWCDbContext dbContext, ConfigManager config)
	{
		_logger = logger;
		_dbContext = dbContext;
		_config = config;
	}
	
	[HttpPost]
	[Route("posttest")]
	public async Task Post()
	{
		// Generates random data and inserts into the database
		var random = new Random();
		int osuId = random.Next();
		ulong discordId = (ulong) Math.Abs(random.NextInt64());
		var user = new OsuUser
		{
			OsuID = osuId,
			DiscordID = discordId,
			RegistrationDate = DateTime.UtcNow
		};
	
		await _dbContext.Users.AddAsync(user);
		await _dbContext.SaveChangesAsync();
		_logger.LogInformation($"Added user {osuId},{discordId} to the database.");
	}

	[HttpPost]
	public async Task Post(int osuId, ulong discordId)
	{
		var user = new OsuUser
		{
			OsuID = osuId,
			DiscordID = discordId,
			RegistrationDate = DateTime.UtcNow
		};

		await _dbContext.Users.AddAsync(user);
		await _dbContext.SaveChangesAsync();
		_logger.LogInformation($"Added user {osuId},{discordId} to the database.");
	}
	
	[HttpPost]
	[Route("discord/{code}")]
	public async Task PostDiscord(string code)
	{
		// Post discord information
		
	}

	[Route("osu")]
	public async Task GetTokenFromCode([FromQuery] string code)
	{
		_logger.LogInformation($"Authorized user. Code received: {code}");

		// Exchange code for token
		
		var client = new HttpClient();
		var values = new Dictionary<string, string>()
		{
			{ "client_id", _config.OsuClientId.ToString() },
			{ "client_secret", _config.OsuClientSecret },
			{ "code", code },
			{ "grant_type", "authorization_code" },
			{ "redirect_uri", "https://auth.stagec.xyz/api/osuauth" }
		};

		var content = new FormUrlEncodedContent(values);
		var response = await client.PostAsync(TokenUrl, content);
		var resString = await response.Content.ReadAsStringAsync();
		_logger.LogInformation(resString);
	}

	[Route("osuauth")]
	public async Task GetUserDataFromToken([FromQuery] string access_token)
	{
		_logger.LogInformation(access_token);
	}

	[HttpGet]
	[Route("osu/{osuId}")]
	public async Task<OsuUser?> Get(int osuID)
	{
		// call the database and return a user
		var match = await _dbContext.Users.FirstOrDefaultAsync(x => x.OsuID == osuID);
		return match;
	}

	[HttpGet]
	[Route("discord/{discordId}")]
	public async Task<OsuUser?> Get(ulong discordId)
	{
		var match = await _dbContext.Users.FirstOrDefaultAsync(x => x.DiscordID == discordId);
		return match;
	}
}