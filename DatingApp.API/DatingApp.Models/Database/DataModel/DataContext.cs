using Microsoft.EntityFrameworkCore;

namespace DatingApp.Models.Database.DataModel;

public class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Photo> Photos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
#if DEBUG
            optionsBuilder.UseSqlite("Data Source=datingapp.db");
#else
            optionsBuilder.UseSqlite(DatabaseConfig.ConnectionString);
#endif
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}