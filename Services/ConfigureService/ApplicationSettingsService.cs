namespace SparkSwim.GoodsService.Services.ConfigureService;

public class ApplicationSettingsService
{
    private readonly IConfiguration _configuration;

    public ApplicationSettingsService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetApplicationUrl()
    {
        return _configuration["ASPNETCORE_URLS"];
    }
}