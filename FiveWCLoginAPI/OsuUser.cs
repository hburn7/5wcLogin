using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FiveWCLoginAPI;

public class OsuRegistrant
{
	[Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	[JsonPropertyName("osu_id")]
	public string OsuID { get; set; }
	[JsonPropertyName("osu_username")]
	public string OsuDisplayName { get; set; }
	[JsonPropertyName("discord_id")]
	public string DiscordID { get; set; }
	[JsonPropertyName("discord_username")]
	public string DiscordDisplayName { get; set; }
	[JsonPropertyName("badges")]
	public string Badges { get; set; }
	[JsonPropertyName("osu_global_rank")]
	public string OsuGlobalRank { get; set; }
	[JsonPropertyName("country_code")]
	public string CountryCode { get; set; }
	[JsonPropertyName("osu_json")]
	public string OsuJson { get; set; }
	public DateTime RegistrationDate { get; set; }
	public override string ToString()
	{
		return $"OsuUser({OsuID}, {OsuDisplayName}, {DiscordID}, {DiscordDisplayName}, {RegistrationDate})";
	}
}

public class ApiReturnUser
{
	[JsonPropertyName("osu_id")]
	public string OsuID { get; set; }
	[JsonPropertyName("osu_username")]
	public string OsuDisplayName { get; set; }
	[JsonPropertyName("discord_id")]
	public string DiscordID { get; set; }
	[JsonPropertyName("discord_username")]
	public string DiscordDisplayName { get; set; }
	[JsonPropertyName("badges")]
	public string Badges { get; set; }
	[JsonPropertyName("osu_global_rank")]
	public string OsuGlobalRank { get; set; }
	[JsonPropertyName("country_code")]
	public string CountryCode { get; set; }
	[JsonPropertyName("registration_date")]
	public DateTime RegistrationDate { get; set; }
	public ApiReturnUser(OsuRegistrant registrant)
	{
		OsuID = registrant.OsuID;
		OsuDisplayName = registrant.OsuDisplayName;
		DiscordID = registrant.DiscordID;
		DiscordDisplayName = registrant.DiscordDisplayName;
		Badges = registrant.Badges;
		OsuGlobalRank = registrant.OsuGlobalRank;
		CountryCode = registrant.CountryCode;
		RegistrationDate = registrant.RegistrationDate;
	}
}
