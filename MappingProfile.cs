using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Models;
using Entities.DataTransferObjects;

namespace MeetupAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Meetup, MeetupDto>();
        }
    }
}
