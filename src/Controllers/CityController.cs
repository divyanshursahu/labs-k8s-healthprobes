
namespace BrezyWeather.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/city")]
[ApiVersion("1.0")]
public class CityController : ControllerBase
{
    private readonly ILogger<CityController> _logger;
    private readonly WeatherDbContext weatherContext;

    public CityController(ILogger<CityController> logger, WeatherDbContext weatherContext)
    {
        _logger = logger;
        weatherContext.Database.EnsureCreated();
        this.weatherContext = weatherContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<City>> GetCities()
    {
        var cities = weatherContext.City;

        if (cities == null)
        {
            return new List<City>();
        }

        return cities.ToList();
    }   
}