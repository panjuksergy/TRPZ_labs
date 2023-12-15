namespace SparkSwim.GoodsService;

public class DbInitializer
{
    public static void Initialize(CinemaDbContext context)
    {
        context.Database.EnsureCreated();       
    }
}