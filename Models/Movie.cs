using SparkSwim.GoodsService.CustomAttributes;

namespace SparkSwim.GoodsService.Goods.Models;

public class Movie
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public DateTime CreationDate { get; set; }
    public string Director { get; set; }
    public Guid CinemaProdId { get; set; }
    public Genre Genre { get; set; }
    public bool IsDeleted { get; set; }
}

public enum Genre
{
    A,
    B,
    C,
    D,
}