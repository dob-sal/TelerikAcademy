namespace Articles.Data
{
    using Articles.Data.Repositories;
    using Articles.Models;

    public interface IArticlesData
    {
        //all the repositories, except Alerts
        //+SaveChanges
        IRepository<Game> Games { get; }
        
        IRepository<Guess> Guesses { get; }

        IRepository<Notification> Notifications { get; }
        
        int SaveChanges();
    }
}
