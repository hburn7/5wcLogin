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
		var result = await _dbContext.Registrants
		                             .AsNoTracking()
		                             .FirstOrDefaultAsync(x => x.OsuID == osuId);
		return result != null ? JsonConvert.SerializeObject(result) : "{}";
	}

	[HttpGet]
	[Route("osu/all")]
	public async Task<string> GetAll([FromQuery] string k)
	{
		if (!await VerifyApiKeyAsync(k))
		{
			return new HttpResponseMessage(HttpStatusCode.Unauthorized).ToString();
		}
		
		var result = await _dbContext.Registrants
		                             .AsNoTracking()
		                             .OrderBy(x => x.RegistrationDate)
		                             .Select(x => new ApiReturnUser(x))
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

	[HttpGet]
	[Route("osu/update")]
	public async Task<string> UpdateExisting([FromQuery]string k)
	{
		if (!await VerifyApiKeyAsync(k))
		{
			return HttpStatusCode.Unauthorized.ToString();
		}
		
		var users = await _dbContext.Registrants
		                            .Where(x => string.IsNullOrEmpty(x.Badges) || string.IsNullOrEmpty(x.CountryCode) || string.IsNullOrEmpty(x.OsuGlobalRank))
		                            .ToListAsync();
		
		var erroredUpdates = new List<ApiReturnUser>();
		
		foreach (var user in users)
		{
			try
			{
				var dJson = JsonConvert.DeserializeObject<dynamic>(user.OsuJson);
				user.Badges = dJson.badges.ToString();
				user.CountryCode = dJson.country_code.ToString();
				user.OsuGlobalRank = dJson.statistics_rulesets.osu.global_rank.ToString();
				
				_logger.LogInformation("Updated user {user} with badges {badges}, country code {countryCode}, and global rank {globalRank}.", 
					user.OsuID, user.Badges, user.CountryCode, user.OsuGlobalRank);

				_dbContext.Registrants.Update(user);
			}
			catch (Exception e)
			{
				_logger.LogWarning($"Failed to parse json dynamically for user {user}", e);
				erroredUpdates.Add(new ApiReturnUser(user));
			}
		}
		await _dbContext.SaveChangesAsync();

		return JsonConvert.SerializeObject(erroredUpdates);
	}
}