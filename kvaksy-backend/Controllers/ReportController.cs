using kvaksy_backend.Data.Models;
using kvaksy_backend.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kvaksy_backend.Controllers
{
    [Authorize(Policy = "IsUser")]
    [ApiController]
    [Route("report")]
    public class ReportController : ControllerBase
    {
        private readonly IReportSessionService _reportSessionService;

        public ReportController(IReportSessionService reportSessionService)
        {
            _reportSessionService = reportSessionService;
        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<List<Report>> GetReportSessions()
        {
            Globals.CheckForUserLevelPermission();

            try
            {
                var reportSessions = _reportSessionService.GetAll();
                return Ok(reportSessions);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<Report> CreateReportSession([FromBody] Report reportSession)
        {
            Globals.CheckForUserLevelPermission();

            try
            {
                var createdReportSession = _reportSessionService.CreateReportSession(reportSession);
                return Ok(createdReportSession);
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
        public ActionResult CreateImage(IFormFile file, Guid id)
        {
            Globals.CheckForUserLevelPermission();

            try
            {
                return Ok(_reportSessionService.UploadImage(id, file));
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
        public ActionResult<List<Report>> GetUnfinishedReportSessions()
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
        public ActionResult<List<Report>> GetFinishedReportSessions()
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
        public ActionResult<List<Report>> FinishReportSession(Guid reportId)
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