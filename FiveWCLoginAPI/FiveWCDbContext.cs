using FiveWCLoginAPI.Config;
using Microsoft.EntityFrameworkCore;

namespace FiveWCLoginAPI;

public class FiveWCDbContext : DbContext
{
	private readonly ConfigManager _config;

	public FiveWCDbContext(ConfigManager config) { _config = config; }
	
	public DbSet<OsuUser> Users { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder builder) => 
		builder.UseNpgsql(_config.GetConnectionString());

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<OsuUser>()
		            .Property(x => x.OsuID)
		            .IsRequired();
		
		modelBuilder.Entity<OsuUser>()
		            .Property(x => x.DiscordID)
		            .IsRequired();
		
		modelBuilder.Entity<OsuUser>()
		            .Property(x => x.RegistrationDate)
		            .IsRequired();
	}
}