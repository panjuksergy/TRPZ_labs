using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SparkSwim.GoodsService.Controllers;
using SparkSwim.GoodsService.Goods.Models;
using SparkSwim.GoodsService.Interfaces;

namespace SparkSwim.GoodsService.Controllers;

public class CinemaController : BaseController
{
    private readonly UnitOfWork _cinemaUnitOfWork;

    public CinemaController(UnitOfWork cinemaUnitOfWork)
    {
        _cinemaUnitOfWork = cinemaUnitOfWork;
    }

    [HttpGet("GetAllProdForCinema")]
    public async Task<ActionResult> GetAllProdForCinema(string cinema)
    {
        return Ok(await _cinemaUnitOfWork.GetAllProdsByCinema(cinema));
    }

    [HttpPost("CreateNewProd")]
    public async Task<ActionResult> CreateNewProd([FromBody] CreateNewProdDto prodDto)
    {
        prodDto.CinemaProd.Id = Guid.NewGuid();
        if (prodDto.Movie != null)
        {
            prodDto.Movie.Id = Guid.NewGuid();

            _cinemaUnitOfWork.CreateCinemaProdWithMovie(prodDto.CinemaProd, prodDto.Movie);
            await _cinemaUnitOfWork.SaveChangesAsync(CancellationToken.None);
        }
        else
        {
            prodDto.Movie.Id = Guid.NewGuid();
            _cinemaUnitOfWork.CreateCinemaProdWithMovie(prodDto.CinemaProd);
        }

        return Ok();
    }

    [HttpPost("CreateMovie")]
    public async Task<ActionResult> CreateMovie([FromBody] Movie movie)
    {
        movie.Id = Guid.NewGuid();
        _cinemaUnitOfWork.CreateMovie(movie);
        await _cinemaUnitOfWork.SaveChangesAsync(CancellationToken.None);
        return Ok();
    }

    [HttpGet("GetAllMovies")]
    public async Task<ActionResult> GetAllMovies()
    {
        var movies = await _cinemaUnitOfWork.GetAllMovies();
        return Ok(movies);
    }

    [HttpPost("GetMoviesByCinema")]
    public async Task<ActionResult> GetMoviesByCinema(string cinemaName)
    {
        var result = await _cinemaUnitOfWork.GetMoviesByCinema(cinemaName);
        return Ok(result);
    }

    [HttpPost("assignProdToAnotherCinema")]
    public async Task<ActionResult> AssignCinemaProdsToAnotherCinema([FromBody]CinemaProdUpdateDTO cinemaProdUpdateDto)
    {
        _cinemaUnitOfWork.AssignCinemaProdsToAnotherCinema(new List<CinemaProd> { cinemaProdUpdateDto.CinemaProd }, cinemaProdUpdateDto.NewAssignCinema);
        await _cinemaUnitOfWork.SaveChangesAsync(CancellationToken.None);
        return Ok();
    }


}