using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SparkSwim.GoodsService.Goods.EntityTypeConfiguration;
using SparkSwim.GoodsService.Goods.Models;
using SparkSwim.GoodsService.Interfaces;

namespace SparkSwim.GoodsService;

public class CinemaDbContext : DbContext, ICinemaDbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<CinemaProd> CinemaProds { get; set; }
    public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.ApplyConfiguration(new MoviesConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}