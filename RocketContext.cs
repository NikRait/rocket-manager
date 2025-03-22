using Microsoft.EntityFrameworkCore;
namespace RocketManager;

public class RocketContext : DbContext
{
    public DbSet<RocketInfo> infos => Set<RocketInfo>();
    public RocketContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Rockets.db");// Путь к файлу базы данных SQLite
    }
}