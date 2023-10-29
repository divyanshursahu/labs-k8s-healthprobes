
namespace BrezyWeather.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/health")]
[ApiVersion("1.0")]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;
    private readonly WeatherDbContext weatherContext;

    public HealthController(ILogger<HealthController> logger, WeatherDbContext weatherContext)
    {
        _logger = logger;
        weatherContext.Database.EnsureCreated();
        this.weatherContext = weatherContext;
    }

    [HttpGet("live")]
    public ActionResult LivenessProbe()
    {
        // Return OK to signal the app is running without any issues. 
        return Ok();
    }

    [HttpGet("ready")]
    public ActionResult ReadinessProbe()
    {
        // Readiness check 1.
        if(weatherContext == null)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, "Service not yet initialized");
        }

        // Any further checks for readiness.
        // ...

        // If all checks are successful, then the app is ready. 
        return Ok();
    }
}