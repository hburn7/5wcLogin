using FiveWCLoginAPI.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FiveWCLoginAPI.Controllers;

[ApiController]
[Route("api")]
public class OsuUserController : ControllerBase
{
	private readonly FiveWCDbContext _dbContext;
	private readonly ILogger<OsuUserController> _logger;

	public OsuUserController(ILogger<OsuUserController> logger, FiveWCDbContext dbContext)
	{
		_logger = logger;
		_dbContext = dbContext;
	}

	[HttpPost]
	public async Task Post(string discordId, string discordDisplay, string osuId, string osuDisplay)
	{
		var user = new OsuUser
		{
			OsuID = osuId,
			OsuDisplayName = osuDisplay,
			DiscordID = discordId,
			DiscordDisplayName = discordDisplay,
			RegistrationDate = DateTime.UtcNow
		};

		await _dbContext.Users.AddAsync(user);
		await _dbContext.SaveChangesAsync();
		_logger.LogInformation($"Added {user} to the database.");
	}

	[HttpGet]
	[Route("osu/{osuId}")]
	public async Task<OsuUser?> Get(string osuID)
	{
		// call the database and return a user
		return await _dbContext.Users.FirstOrDefaultAsync(x => x.OsuID == osuID);
	}

	[HttpGet]
	[Route("ping")]
	public async Task<HttpStatusCode> GetPing()
	{
		return HttpStatusCode.OK;
	}
}