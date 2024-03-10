using kvaksy_backend.Data.Models;
using kvaksy_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace kvaksy_backend.Controllers
{
    [ApiController]
    [Route("report")]
    public class ReportController : ControllerBase
    {
        private readonly IReportSessionServices _reportSessionService;

        public ReportController(IReportSessionServices reportSessionService)
        {
            _reportSessionService = reportSessionService;
        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<List<Report>> GetReport()
        {
            Globals.CheckForUserLevelPermission();

            try
            {
                var reportSessions = _reportSessionService.GetAll();
                var jsonString = "";

                foreach ( var reportSession in reportSessions )
                {
                    jsonString += reportSession.ToJson();
                }

                return Ok(jsonString);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<Report> CreateReport()
        {
            Globals.CheckForUserLevelPermission();

            try
            {
                var createdReportSession = _reportSessionService.CreateReport();

                return Ok(createdReportSession.ToJson());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("image")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> AddImageToReport(IFormFile file, Guid id)
        {
            Globals.CheckForUserLevelPermission();

            try
            {
                return Ok(await _reportSessionService.AddImageToReport(id, file));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("unfinished")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<List<Report>> GetUnfinishedReport()
        {
            Globals.CheckForUserLevelPermission();

            try
            {
                var reports = _reportSessionService.GetUnfinishedReportSessions();
                return Ok(reports);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("finished")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<List<Report>> GetFinishedReport()
        {
            Globals.CheckForUserLevelPermission();

            try
            {
                var reports = _reportSessionService.GetFinishedReportSessions();
                return Ok(reports);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("finished")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<List<Report>> FinishReport(Guid reportId)
        {
            Globals.CheckForUserLevelPermission();

            try
            {
                var finished = _reportSessionService.FinishReportSession(reportId);
                if (finished == null)
                    return BadRequest("Report from body was not found");

                return Ok(finished);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}