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
        private readonly ITemperatureServices _temperatureServices;
        private readonly IWeightServices _weightServices;
        public ReportFieldController(ITemperatureServices temperatureServices, IWeightServices weightServices)
        { 
            _weightServices = weightServices;
            _temperatureServices = temperatureServices;
        }

        [Route("temperature")]
        [HttpPatch]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Report>> UpdateTemperatureInReport(Guid id, [FromBody] double temperature)
        {
            Globals.CheckForUserLevelPermission();

            try
            {
                var result = await _temperatureServices.updateTemperatureOnReport(id, temperature);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("weight")]
        [HttpPatch]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Report>> UpdateWeightInReport(Guid id, [FromBody] double weight)
        {
            Globals.CheckForUserLevelPermission();

            try
            {
                var result = await _weightServices.updateWeightOnReport(id, weight);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
