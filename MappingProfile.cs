using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace MeetupAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Meetup, MeetupDto>();
            CreateMap<Account, AccountDto>();
            CreateMap<MeetupForCreationDto, Meetup>();
            CreateMap<MeetupForUpdateDto, Meetup>();
        }
    }
}
