using FiveWCLoginAPI.Config;
using Microsoft.EntityFrameworkCore;

namespace FiveWCLoginAPI;

public class FiveWCDbContext : DbContext
{
	private readonly ConfigManager _config;

	public FiveWCDbContext(ConfigManager config) { _config = config; }
	
	public DbSet<OsuRegistrant> Registrants { get; set; } = null!;
	public DbSet<AuthorizedUser> AuthorizedUsers { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder builder) => 
		builder.UseNpgsql(_config.GetConnectionString());

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<OsuRegistrant>()
		            .Property(x => x.OsuID)
		            .IsRequired();
		
		modelBuilder.Entity<OsuRegistrant>()
		            .Property(x => x.DiscordID)
		            .IsRequired();
		
		modelBuilder.Entity<OsuRegistrant>()
		            .Property(x => x.RegistrationDate)
		            .IsRequired();
	}
}