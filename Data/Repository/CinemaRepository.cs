using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SparkSwim.GoodsService.Goods.Models;
using SparkSwim.GoodsService.Interfaces;

namespace SparkSwim.GoodsService;

public class CinemaRepository : IRepository<CinemaProd>
{
    private readonly ICinemaDbContext _cinemaDb;
    
    public CinemaRepository(ICinemaDbContext cinemaDbContext) => _cinemaDb = cinemaDbContext;


    public async Task<IEnumerable<CinemaProd>> GetList(Expression<Func<CinemaProd, bool>> condition)
    {
        var res = await _cinemaDb.CinemaProds.Where(condition).ToListAsync();
        return res;
    }

    public async Task<CinemaProd> FindByIdAsync(Guid id)
    {
        var res = await _cinemaDb.CinemaProds.FirstOrDefaultAsync(_ => _.Id == id);
        return res;
    }

    public async Task<CinemaProd> FindByTitleAsync(string title)
    {
        var res = await _cinemaDb.CinemaProds.FirstOrDefaultAsync(_ => _.Movie.Title == title);
        return res;
    }

    public async Task<List<CinemaProd>> FetchAllProdsByCinemaAsync(string cinemaName)
    {
        var res = await _cinemaDb.CinemaProds.Where(_ => _.AssignCinema == cinemaName).ToListAsync();
        return res;
    }
    
    public async void Create(IEnumerable<CinemaProd> entities)
    {
        await _cinemaDb.CinemaProds.AddRangeAsync(entities);
    }

    public void Update(IEnumerable<CinemaProd> entities)
    {
        _cinemaDb.CinemaProds.UpdateRange(entities);
    }

    public void SoftDelete(IEnumerable<CinemaProd> entities)
    {
        foreach (var movie in entities)
        {
            movie.IsDeleted = true;
        }

        _cinemaDb.CinemaProds.UpdateRange(entities);
    }

    public void HardDelete(IEnumerable<CinemaProd> entities)
    {
        _cinemaDb.CinemaProds.RemoveRange(entities);
    }

    public void SaveChanges() => _cinemaDb.SaveChangesAsync(CancellationToken.None);
}