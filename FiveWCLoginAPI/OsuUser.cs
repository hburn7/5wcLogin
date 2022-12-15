using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiveWCLoginAPI;

public class OsuRegistrant
{
	[Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	[JsonProperty("osu_id")]
	public string OsuID { get; set; }
	[JsonProperty("osu_username")]
	public string OsuDisplayName { get; set; }
	[JsonProperty("discord_id")]
	public string DiscordID { get; set; }
	[JsonProperty("discord_username")]
	public string DiscordDisplayName { get; set; }
	public DateTime RegistrationDate { get; set; }
	public override string ToString()
	{
		return $"OsuUser({OsuID}, {OsuDisplayName}, {DiscordID}, {DiscordDisplayName}, {RegistrationDate})";
	}
}