namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IMeetupRepository Meetup { get; }

        IAccountRepository Account { get; }
        void Save();

    }
}
