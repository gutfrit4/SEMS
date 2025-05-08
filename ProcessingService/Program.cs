using Microsoft.EntityFrameworkCore;
using ProcessingService.Data;
using ProcessingService.Interfaces;
using ProcessingService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISensorDataService, SensorDataService>();

var app = builder.Build();

// Configure
app.UseAuthorization();
app.MapControllers();

app.Run();