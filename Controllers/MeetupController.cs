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

        [HttpGet("id")]
        public IActionResult GetMeetupById (Guid id)
        {
            try
            {
                var meetup = _repository.Meetup.GetMeetupById(id);
                return NotFound();

                if (meetup==null)
                {
                    _logger.LogError($"Meetup with id: {id}, hasn't been found in db.");
                }
                else
                {
                    _logger.LogInfo($"Returned meetup with id: {id}");

                    var meetupResult = _mapper.Map<MeetupDto>(meetup);
                    return Ok(meetupResult);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetMeetupById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
