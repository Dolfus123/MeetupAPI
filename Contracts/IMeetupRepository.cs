using Entities.Models;
using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IMeetupRepository : IRepositoryBase<Meetup>
    {
        IEnumerable<Meetup> GetAllMeetups();
        Meetup GetMeetupById(Guid meetupId);
        Meetup GetMeetupWithDetails(Guid meetupId);
        void CreateMeetup(Meetup meetup);
        void UpdateMeetup(Meetup meetup);
        void DeleteMeetup(Meetup meetup);

    }
}
