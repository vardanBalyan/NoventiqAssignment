using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace NoventiqAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizerController : ControllerBase
    {
        private readonly IStringLocalizer<LocalizerController> _localizer;

        public LocalizerController(IStringLocalizer<LocalizerController> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet("greet")]
        public async ValueTask<IActionResult> GreetAsync([FromQuery, SwaggerParameter("Language code. Allowed values: en, fr, es. Defaults to en")] string lang = "en")
        {
            var message = _localizer["Hello"].Value;
            return Ok(new { Message = message });
        }

        [HttpGet("app")]
        public async ValueTask<IActionResult> AppAsync([FromQuery, SwaggerParameter("Language code. Allowed values: en, fr, es, hi. Defaults to en")] string lang = "en")
        {
            var message = _localizer["This is a Noventiq Assignment app for DOTNET."].Value;
            return Ok(new { Message = message });
        }
    }
}
