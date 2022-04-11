using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using AutoMapper;
using Entities.DataTransferObjects;

namespace MeetupAPI.Controllers
{
    [Route("api/meetup")]
    [ApiController]
    public class MeetupController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public MeetupController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllMeetups()
        {
            try
            {
                var meetups = _repository.Meetup.GetAllMeetups();
                _logger.LogInfo($"Returned all meetups from database.");

                var meetupResult = _mapper.Map<IEnumerable<MeetupDto>>(meetups);

                return Ok(meetupResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllMeetups action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
