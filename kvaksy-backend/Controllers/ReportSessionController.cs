using kvaksy_backend.Data.Models;
using kvaksy_backend.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kvaksy_backend.Controllers
{
    [Authorize(Policy = "IsUser")]
    [ApiController]
    [Route("reportSession")]
    public class ReportSessionController : ControllerBase
    {
        private readonly IReportSessionService _reportSessionService;

        public ReportSessionController(IReportSessionService reportSessionService)
        {
            _reportSessionService = reportSessionService;
        }

        [Route("all")]
        [HttpGet]
        public ActionResult<List<ReportSession>> GetAll()
        {
            try
            {
                var reportSessions = _reportSessionService.GetAll();
                return Ok(reportSessions);
            }
            catch (System.Exception)
            {
                return StatusCode(404);
            }
        }

        [Route("")]
        [HttpPost]
        public ActionResult<ReportSession> CreateReportSession([FromBody] ReportSession reportSession)
        {
            try
            {
                var createdReportSession = _reportSessionService.CreateReportSession(reportSession);
                return Ok(createdReportSession);
            }
            catch (System.Exception)
            {
                return StatusCode(400);
            }
        }
        [Route("image")]
        [HttpPost]
        public ActionResult UploadImage(IFormFile file, Guid id)
        {
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
        public ActionResult<List<ReportSession>> GetUnfinishedReportSessions()
        {
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
        public ActionResult<List<ReportSession>> GetFinishedReportSessions()
        {
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
        public ActionResult<List<ReportSession>> FinishReportSessions(Guid reportId)
        {
            try
            {
                var finished = _reportSessionService.FinishReportSession(reportId);
                if (finished == null)
                    return StatusCode(400);

                return Ok(finished);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}