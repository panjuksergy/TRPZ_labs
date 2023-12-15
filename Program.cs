using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SparkSwim.GoodsService;
using SparkSwim.GoodsService.Goods.Models;
using SparkSwim.GoodsService.Interfaces;
using SparkSwim.GoodsService.ShortenerService;

var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder.Services);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<CinemaDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
    }
}

Configure(app);

app.Run();

void RegisterServices(IServiceCollection services)
{
    services.AddControllers();

    services.AddDbContext<ICinemaDbContext, CinemaDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

    services.AddScoped<IRepository<CinemaProd>, CinemaRepository>();
    services.AddScoped<IRepository<Movie>, MovieRepository>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<UnitOfWork>();
    services.AddApplication();
    services.AddControllers();
}

void Configure(IApplicationBuilder app)
{
    // app.UseHttpsRedirection();
    app.UseRouting();
    app.UseCors(_ => { _.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();});
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
}