using FiveWCLoginAPI;
using FiveWCLoginAPI.Config;
using Microsoft.EntityFrameworkCore;
using OsuSharp;
using OsuSharp.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<FiveWCDbContext>();
builder.Services.AddSingleton<IConfigManager, ConfigManager>();
builder.Services.AddSingleton<IOsuService, OsuService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	var context = services.GetRequiredService<FiveWCDbContext>();
	if (context.Database.GetPendingMigrations().Any())
	{
		context.Database.Migrate();
	}
}

app.Run();