using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class MeetupRepository : RepositoryBase<Meetup>, IMeetupRepository
    {
        public MeetupRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<Meetup> GetAllMeetups()
        {
            return FindAll()
                .OrderBy(mup => mup.Title)
                .ToList();
        }
        public Meetup GetMeetupById(Guid meetupId)
        {
            return FindByCondition(meetup => meetup.Id.Equals(meetupId))
                .FirstOrDefault(); 
        }
        public Meetup GetMeetupWithDetails(Guid meetupId)
        {
            return FindByCondition(meetup => meetup.Id.Equals(meetupId))
                .Include(ac => ac.Accounts)
                .FirstOrDefault();
        }
        public void CreateMeetup(Meetup meetup) => Create(meetup);

        public void UpdateMeetup(Meetup meetup) => Update(meetup);

        public void DeleteMeetup(Meetup meetup) => Delete(meetup);
    }
}
