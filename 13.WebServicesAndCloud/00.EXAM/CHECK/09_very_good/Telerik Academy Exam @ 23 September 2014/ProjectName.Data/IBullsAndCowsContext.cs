namespace BullsAndCows.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using BullsAndCows.Model;

    public interface IBullsAndCowsContext
    {
        IDbSet<Game> Games { get; set; }

        IDbSet<Notification> Notifications { get; set; }

        IDbSet<Guess> Guesses { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        int SaveChanges();
    }
}
