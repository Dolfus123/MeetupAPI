using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
        public IEnumerable<Account> AccountsByMeetup(Guid meetupId)
        {
            return FindByCondition(a => a.MeetupId.Equals(meetupId)).ToList();
        }
    }
}
