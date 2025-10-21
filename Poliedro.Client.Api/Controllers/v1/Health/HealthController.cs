using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Swashbuckle.AspNetCore.Annotations;

namespace Poliedro.Client.Api.Controllers.v1.Health
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;

        public HealthController(HealthCheckService healthCheckService)
      {
  _healthCheckService = healthCheckService;
        }

        [SwaggerOperation(Summary = "Get application health status")]
        [SwaggerResponse(StatusCodes.Status200OK, "The application is healthy", typeof(HealthCheckResult))]
      [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "The application is unhealthy", typeof(HealthCheckResult))]
     [HttpGet]
        public async Task<IActionResult> Get()
        {
      var result = await _healthCheckService.CheckHealthAsync();
    
          var response = new
         {
        Status = result.Status.ToString(),
    TotalDuration = result.TotalDuration,
       Entries = result.Entries.Select(e => new
   {
        Name = e.Key,
  Status = e.Value.Status.ToString(),
     Duration = e.Value.Duration,
        Description = e.Value.Description,
         Tags = e.Value.Tags
  })
         };

  return result.Status == HealthStatus.Healthy 
   ? Ok(response) 
 : StatusCode(StatusCodes.Status503ServiceUnavailable, response);
     }

      [SwaggerOperation(Summary = "Get ready status")]
        [SwaggerResponse(StatusCodes.Status200OK, "The application is ready")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "The application is not ready")]
        [HttpGet("ready")]
 public async Task<IActionResult> Ready()
        {
         var result = await _healthCheckService.CheckHealthAsync(healthCheck => healthCheck.Tags.Contains("ready"));
      
        return result.Status == HealthStatus.Healthy 
       ? Ok(new { Status = "Ready", Timestamp = DateTime.UtcNow })
        : StatusCode(StatusCodes.Status503ServiceUnavailable, 
 new { Status = "Not Ready", Timestamp = DateTime.UtcNow });
    }

   [SwaggerOperation(Summary = "Get live status")]
      [SwaggerResponse(StatusCodes.Status200OK, "The application is alive")]
      [HttpGet("live")]
        public IActionResult Live()
        {
       return Ok(new { Status = "Alive", Timestamp = DateTime.UtcNow });
        }
    }
}