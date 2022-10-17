using FiveWCLoginAPI.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiveWCLoginAPI.Controllers;

[ApiController]
[Route("api")]
public class OsuUserController : ControllerBase
{
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

	[HttpPost]
	[Route("osu")]
	public async Task GetFromCode([FromQuery] string code)
	{
		_logger.LogInformation(code);
		string baseUrl = "https://osu.ppy.sh/oauth/token";
		
		// todo: come back and request the osu api bla bla bla
	}

	[HttpGet]
	[Route("{osuId}")]
	public async Task<OsuUser?> Get(int osuID)
	{
		// call the database and return a user
		var match = await _dbContext.Users.FirstOrDefaultAsync(x => x.OsuID == osuID);
		return match;
	}

	[HttpGet]
	[Route("osu")]
	public async Task<List<OsuUser>> GetAllUsers()
	{
		var users = await _dbContext.Users.ToListAsync();
		return users;
	}

	[HttpGet]
	[Route("discord/{discordId}")]
	public async Task<OsuUser?> Get(ulong discordId)
	{
		var match = await _dbContext.Users.FirstOrDefaultAsync(x => x.DiscordID == discordId);
		return match;
	}
}