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
	private readonly IConfigManager _config;
	private readonly IOsuService _osuService;

	public OsuUserController(ILogger<OsuUserController> logger, FiveWCDbContext dbContext, IConfigManager config, IOsuService osuService)
	{
		_logger = logger;
		_dbContext = dbContext;
		_config = config;
		_osuService = osuService;
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
		var token = await _osuService.ResolveTokenAsync(code);
		var user = await _osuService.ResolveUserAsync();
		_logger.LogInformation(token.AccessToken);
		_logger.LogInformation(user.Id.ToString());
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