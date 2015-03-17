using BullAndCows.Data.Repositories;
using BullAndCows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullAndCows.Data
{
    public interface IBullAndCowsData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Game> Games { get; }

        IRepository<Notification> Notifications { get; }

        IRepository<Guess> Guesses { get; }

        int SaveChanges();
    }
}
