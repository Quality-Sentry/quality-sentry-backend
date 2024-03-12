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
        private readonly IImageServices _imageServices;
        public ReportFieldController(ITemperatureServices temperatureServices, IWeightServices weightServices, IImageServices imageServices)
        { 
            _weightServices = weightServices;
            _temperatureServices = temperatureServices;
            _imageServices = imageServices;
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

        [Route("image")]
        [HttpPatch]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Report>> UpdateImagesInReport(Guid id, IFormFile file)
        {
            Globals.CheckForUserLevelPermission();

            try
            {
                var result = await _imageServices.AddImageToReport(id, file);

                switch (result.Changes)
                {
                    case 0:
                        return StatusCode(200, new ApiResponse("Attempt was successful, but no changes were made", result.Model));

                    case 1:
                        return StatusCode(201, new ApiResponse(result.Model));

                    default:
                        return StatusCode(500, new ApiResponse("An unexpected error happened"));
                }
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
