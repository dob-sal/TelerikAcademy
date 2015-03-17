namespace Application.Data.Contracts
{
    using Application.Models;

    public interface IApplicationData
    {
        IRepository<Game> Games { get; }

        IRepository<Score> Scores { get; }

        IRepository<Color> Colors { get; }

        IRepository<Guess> Guesses { get; }

        IRepository<Nodification> Notifications { get; }

        void SaveChanges();
    }
}