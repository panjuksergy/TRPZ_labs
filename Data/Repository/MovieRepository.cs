using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SparkSwim.GoodsService.Goods.Models;
using SparkSwim.GoodsService.Interfaces;

namespace SparkSwim.GoodsService;

public class MovieRepository : IRepository<Movie>
{
    private readonly ICinemaDbContext _cinemaDb;
    
    public MovieRepository(ICinemaDbContext cinemaDbContext) => _cinemaDb = cinemaDbContext;


    public async Task<IEnumerable<Movie>> GetList(Expression<Func<Movie, bool>> condition)
    {
        var res = await _cinemaDb.Movies.Where(condition).ToListAsync();
        return res;
    }

    public async Task<Movie> FindByIdAsync(Guid id)
    {
        var res = await _cinemaDb.Movies.FirstOrDefaultAsync(_ => _.Id == id);
        return res;
    }

    public async Task<Movie> FindByTitleAsync(string title)
    {
        var res = await _cinemaDb.Movies.FirstOrDefaultAsync(_ => _.Title == title);
        return res;
    }
    
    public async void Create(IEnumerable<Movie> entities)
    {
        await _cinemaDb.Movies.AddRangeAsync(entities);
    }

    public void Update(IEnumerable<Movie> entities)
    {
        _cinemaDb.Movies.UpdateRange(entities);
    }

    public void SoftDelete(IEnumerable<Movie> entities)
    {
        foreach (var movie in entities)
        {
            movie.IsDeleted = true;
        }

        _cinemaDb.Movies.UpdateRange(entities);
    }

    public void HardDelete(IEnumerable<Movie> entities)
    {
        _cinemaDb.Movies.RemoveRange(entities);
    }

    public void SaveChanges() => _cinemaDb.SaveChangesAsync(CancellationToken.None);
}