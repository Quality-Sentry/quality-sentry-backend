using kvaksy_backend.data.models;
using kvaksy_backend.Data.Models;
using kvaksy_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace kvaksy_backend.Controllers
{
    [ApiController]
    [Route("reportField")]

    public class ReportFieldController : ControllerBase
    {
        private readonly ITemperatureServices _services;
        public ReportFieldController(ITemperatureServices services) 
        { 
            _services = services;
        }

        [Route("temperature")]
        [HttpPatch]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Report>> UpdateTemperatureInReport(Guid id, [FromBody] int temperature)
        {
            Globals.CheckForUserLevelPermission();

            try
            {
                var result = await _services.updateTemperatureOnReport(id, temperature);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
