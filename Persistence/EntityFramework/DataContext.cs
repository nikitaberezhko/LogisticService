using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFramework;

public class DataContext : DbContext
{
    public DbSet<Container> Containers { get; set; }
    
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Container>().Property(x => x.Id).HasColumnName("id");
        modelBuilder.Entity<Container>().Property(x => x.OrderId).HasColumnName("order_id");
        modelBuilder.Entity<Container>().Property(x => x.Latitude).HasColumnName("latitude");
        modelBuilder.Entity<Container>().Property(x => x.Longitude).HasColumnName("longitude");
        modelBuilder.Entity<Container>().Property(x => x.LastUpdateTime).HasColumnName("last_update_time");
    }
}