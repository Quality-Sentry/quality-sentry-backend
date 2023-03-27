using kvaksy_backend.models;
using kvaksy_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace kvaksy_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportSessionController : ControllerBase
    {
        private readonly IReportSessionService _reportSessionRepository;

        public ReportSessionController(IReportSessionService reportSessionRepository)
        {
            _reportSessionRepository = reportSessionRepository;
        }

        [Route("All")]
        [HttpGet]
        public ActionResult<List<ReportSession>> GetAll()
        {
            try
            {
                var reportSessions = _reportSessionRepository.GetAll();
                return Ok(reportSessions);
            }
            catch (System.Exception)
            {
                return StatusCode(404);
            }
        }
    }
}