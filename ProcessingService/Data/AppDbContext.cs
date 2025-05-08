using Microsoft.EntityFrameworkCore;
using ProcessingService.Models;

namespace ProcessingService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<SensorData> SensorData { get; set; }

}