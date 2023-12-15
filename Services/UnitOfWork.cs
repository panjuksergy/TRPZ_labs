using Microsoft.EntityFrameworkCore;
using SparkSwim.GoodsService;
using SparkSwim.GoodsService.Goods.Models;
using SparkSwim.GoodsService.Interfaces;

public class UnitOfWork : IUnitOfWork
{
    private readonly ICinemaDbContext _cinemaDb;
    private readonly IRepository<Movie> _moviewRepo;
    private readonly IRepository<CinemaProd> _cinemaProdRepo;
    
    public UnitOfWork(ICinemaDbContext cinemaDbContext, IRepository<Movie> moviewRepo, IRepository<CinemaProd> cinemaProdsRepo)
    {
        _cinemaDb = cinemaDbContext;
        _moviewRepo = moviewRepo;
        _cinemaProdRepo = cinemaProdsRepo;
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _cinemaDb.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<CinemaProd>> GetAllProdsByCinema(string cinema)
    {
        return await _cinemaProdRepo.GetList(_ => _.AssignCinema == cinema);
    }

    public async Task<IEnumerable<Movie>> GetAllMovies()
    {
        return await _moviewRepo.GetList(_ => _.IsDeleted != true);
    }
    
    public void CreateCinemaProdWithMovie(CinemaProd cinemaProd, Movie movie = null)
    {
        if (movie == null)
        {
            _cinemaProdRepo.Create(new List<CinemaProd>{cinemaProd});
        }
        else
        {
            cinemaProd.Movie = movie;
            _cinemaProdRepo.Create(new List<CinemaProd> {cinemaProd});
        }
    }

    public async Task<List<Movie>> GetMoviesByCinema(string cinemaName)
    {
        var prods = await _cinemaProdRepo.GetList(_ => _.AssignCinema == cinemaName);
        var movies = prods.Select(_ => _.Movie).ToList();
        return movies;
    }

    public void CreateMovie(Movie movies)
    {
        _moviewRepo.Create(new List<Movie>{movies});
    }

    public void DeleteCinemaProdWithMovieSoft(CinemaProd cinemaProd)
    {
        cinemaProd.IsDeleted = true;
        _cinemaProdRepo.Update(new List<CinemaProd>{cinemaProd});
    }

    public void DeleteCinemaProdWithMovieHard(CinemaProd cinemaProd)
    {
        _cinemaProdRepo.HardDelete(new List<CinemaProd>{cinemaProd});
    }
    
    public async Task<IList<Movie>> GetAllMoviesByGenre(Genre genre)
    {
        return await _cinemaDb.Movies.Where(_ => _.Genre == genre).ToListAsync();
    }
    
    public void CreateMovies(IList<Movie> movies)
    {
        _moviewRepo.Create(movies);
    }

    public void AssignMoviesToCinemaProds(IList<Movie> movies, IList<CinemaProd> cinemaProds)
    {
        for (int i = 0; i < movies.Count && i < cinemaProds.Count; i++)
        {
            cinemaProds[i].Movie = movies[i];
        }

        _cinemaProdRepo.Update(cinemaProds);
    }

    public void AssignCinemaProdsToAnotherCinema(IList<CinemaProd> cinemaProds, string newCinema)
    {
        foreach (var cinemaProd in cinemaProds)
        {
            cinemaProd.AssignCinema = newCinema;
        }

        _cinemaProdRepo.Update(cinemaProds);
    }

    public void ChangeProdDurations(IList<CinemaProd> cinemaProds, DateTime newDurations)
    {
        for (int i = 0; i < cinemaProds.Count; i++)
        {
            cinemaProds[i].DateFinish = newDurations;
        }

        _cinemaProdRepo.Update(cinemaProds);
    }
}