namespace SparkSwim.GoodsService.Goods.Models;

public class CinemaProd
{
    public Guid Id { get; set; }
    public Movie Movie { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateFinish { get; set; }
    public string AssignCinema { get; set; }
    public bool IsDeleted { get; set; }
}

public class CinemaProdUpdateDTO
{
    public CinemaProd CinemaProd { get; set; }
    public string NewAssignCinema { get; set; }
}