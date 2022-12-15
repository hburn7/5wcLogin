using System.ComponentModel.DataAnnotations;

namespace FiveWCLoginAPI;

public class OsuUser
{
	[Key]
	public string OsuID { get; set; }
	public string OsuDisplayName { get; set; }
	public string DiscordID { get; set; }
	public string DiscordDisplayName { get; set; }
	public DateTime RegistrationDate { get; set; }
}