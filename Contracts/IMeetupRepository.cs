using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
