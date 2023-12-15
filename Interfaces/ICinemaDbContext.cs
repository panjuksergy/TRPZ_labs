using Microsoft.EntityFrameworkCore;
using SparkSwim.GoodsService.Goods.Models;

namespace SparkSwim.GoodsService.Interfaces;

public interface ICinemaDbContext
{
    DbSet<Movie> Movies { get; set; }
    DbSet<CinemaProd> CinemaProds { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}