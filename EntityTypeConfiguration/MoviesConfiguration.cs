using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SparkSwim.GoodsService.Goods.Models;

namespace SparkSwim.GoodsService.Goods.EntityTypeConfiguration;

public class MoviesConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(_ => _.Id);
    }   
}