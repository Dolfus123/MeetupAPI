using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;

namespace MeetupAPI.Controllers
{
    [Route("api/meetup")]
    [ApiController]
    public class MeetupController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        public MeetupController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetAllMeetups()
        {
            try
            {
                var meetups = _repository.Meetup.GetAllMeetups();
                _logger.LogInfo($"Returned all meetups from database.");
                return Ok(meetups);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllMeetups action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
