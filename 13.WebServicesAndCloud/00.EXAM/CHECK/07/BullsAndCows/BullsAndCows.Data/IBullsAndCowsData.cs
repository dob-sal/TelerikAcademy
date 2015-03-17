using BullsAndCows.Data.Repositories;
using BullsAndCows.Models;
using System;
namespace BullsAndCows.Data
{
    public interface IBullsAndCowsData
    {
        IRepository<Game> Games { get; }
        IRepository<Guess> Guesses { get; }
        IRepository<Notification> Notifications { get; }
        IRepository<ApplicationUser> Users { get; }

        int SaveChanges();
    }
}
