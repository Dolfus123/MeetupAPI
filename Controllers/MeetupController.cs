using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

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

        [HttpGet("{id}", Name = "MeetupById") ]
        public IActionResult GetMeetupById (Guid id)
        {
            try
            {
                var meetup = _repository.Meetup.GetMeetupById(id);
                

                if (meetup is null)
                {
                    _logger.LogError($"Meetup with id: {id}, hasn't been found in db.");
                    return NotFound();
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

        [HttpGet("{id}/account")]
        public IActionResult GetMeetupWithDetails(Guid id)
        {
            try
            {
                var meetup = _repository.Meetup.GetMeetupWithDetails(id);
                if (meetup == null)
                {
                    _logger.LogError($"Meetup with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned meetup with details for id: {id}");

                    var meetupResult = _mapper.Map<MeetupDto>(meetup);
                    return Ok(meetupResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetMeetupWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateMeetup([FromBody] MeetupForCreationDto meetup)
        {
            try
            {
                if (meetup is null)
                {
                    _logger.LogError("Meetup object sent from client is null.");
                    return BadRequest("Meetup object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid meetup object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var meetupEntity = _mapper.Map<Meetup>(meetup);
                _repository.Meetup.CreateMeetup(meetupEntity);
                _repository.Save();
                var createdMeetup = _mapper.Map<MeetupDto>(meetupEntity);
                return CreatedAtRoute("MeetupById", new { id = createdMeetup.Id }, createdMeetup);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateMeetup action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMeetup(Guid id, [FromBody] MeetupForUpdateDto meetup)
        {
            try
            {
                if (meetup is null)
                {
                    _logger.LogError("Meetup object sent from client is null.");
                    return BadRequest("Meetup object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid meetup object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var meetupEntity = _repository.Meetup.GetMeetupById(id);
                if (meetupEntity is null)
                {
                    _logger.LogError($"Meetup with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(meetup, meetupEntity);
                _repository.Meetup.UpdateMeetup(meetupEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateMeetup action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMeetup(Guid id)
        {
            try
            {
                var meetup = _repository.Meetup.GetMeetupById(id);
                if (meetup == null)
                {
                    _logger.LogError($"Meetup with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                if (_repository.Account.AccountsByMeetup(id).Any())
                {
                    _logger.LogError($"Cannot delete meetup with id: {id}. It has related accounts. Delete those accounts first");
                    return BadRequest("Cannot delete meetup. It has related accounts. Delete those accounts first");
                }
                _repository.Meetup.DeleteMeetup(meetup);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteMeetup action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
