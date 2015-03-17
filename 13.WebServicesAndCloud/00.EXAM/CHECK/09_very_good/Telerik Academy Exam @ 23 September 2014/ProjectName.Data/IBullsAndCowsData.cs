namespace BullsAndCows.Data
{
    using BullsAndCows.Data.Ropositories;
    using BullsAndCows.Model;

    public interface IBullsAndCowsData
    {
        IGenericRepository<Game> Games { get; }

        IGenericRepository<Notification> Notifications { get; }

        IGenericRepository<Guess> Guesses { get; }

        IGenericRepository<User> Users { get; }

        void SaveChanges();
    }
}
