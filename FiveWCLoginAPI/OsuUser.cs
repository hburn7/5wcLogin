using System.ComponentModel.DataAnnotations;

namespace FiveWCLoginAPI;

public class OsuUser
{
	[Key]
	public int OsuID { get; set; }
	public ulong DiscordID { get; set; }
	public DateTime RegistrationDate { get; set; }
}