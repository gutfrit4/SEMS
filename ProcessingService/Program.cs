using Microsoft.EntityFrameworkCore;
using ProcessingService.Data;
using ProcessingService.Interfaces;
using ProcessingService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

var app = builder.Build();

app.Urls.Add("http://0.0.0.0:80");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Database.GetPendingMigrations().Any())
    {
        Console.WriteLine("✅ No pending migrations.");
    }
    else
    {
        Console.WriteLine("⚙️ Applying pending migrations...");
        db.Database.Migrate();
    }
}


app.MapControllers();
app.Run();