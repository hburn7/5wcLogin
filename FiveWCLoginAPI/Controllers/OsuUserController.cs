using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
	[Route("register")]
	public async Task<HttpResponseMessage> Register([FromQuery] string k, [FromBody]OsuRegistrant registrant)
	{
		_logger.LogInformation($"Attempting to register user {registrant} with key {k}.");
		if (!await VerifyApiKeyAsync(k))
		{
			_logger.LogWarning($"Key verification failed for {k}");
			return new HttpResponseMessage(HttpStatusCode.Unauthorized);
		}
		
		var userExists = await _dbContext.Registrants.AnyAsync(x => x.OsuID == registrant.OsuID);
		if (userExists)
		{
			_logger.LogInformation($"User {registrant.OsuID} already exists in the database.");
			return new HttpResponseMessage(HttpStatusCode.Conflict);
		}

		registrant.RegistrationDate = DateTime.UtcNow;

		_logger.LogInformation($"Adding user {registrant.OsuID} to the database.");
		await _dbContext.Registrants.AddAsync(registrant);
		await _dbContext.SaveChangesAsync();
		
		return new HttpResponseMessage(HttpStatusCode.OK);
	}

	[HttpGet]
	[Route("osu")]
	public async Task<string> Get([FromQuery] string k, [FromQuery] string osuId)
	{
		if (!await VerifyApiKeyAsync(k))
		{
			return new HttpResponseMessage(HttpStatusCode.Unauthorized).ToString();
		}
		
		// call the database and return a user
		var result = await _dbContext.Registrants.FirstOrDefaultAsync(x => x.OsuID == osuId);
		if (result != null)
		{
			return JsonConvert.SerializeObject(result);
		}

		return "{}";
	}

	[HttpGet]
	[Route("osu/all")]
	public async Task<string> GetAll([FromQuery] string k)
	{
		if (!await VerifyApiKeyAsync(k))
		{
			return new HttpResponseMessage(HttpStatusCode.Unauthorized).ToString();
		}
		
		var result = await _dbContext.Registrants.OrderBy(x => x.RegistrationDate)
		                             .Select(x => new Tuple<string, string, string>(x.OsuID, x.DiscordID, x.DiscordDisplayName))
		                             .ToListAsync();
		return result.Any() ? JsonConvert.SerializeObject(result) : "{}";
	}

	private async Task<bool> VerifyApiKeyAsync(string key) => await _dbContext.AuthorizedUsers.AnyAsync(x => x.ApiKey == key);

	[HttpGet]
	[Route("ping")]
	public async Task<HttpStatusCode> GetPing()
	{
		return HttpStatusCode.OK;
	}
}