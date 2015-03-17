using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BullsAndCows.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace BullsAndCows.Data
{
    public interface IBullsAndCowsDbContext
    {
        IDbSet<Game> Games { get; set; }

        IDbSet<Guess> Guesses { get; set; }

        IDbSet<Notification> Notifications { get; set; }

        IDbSet<ApplicationUser> Users { get; set; }
        
        void SaveChanges();

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
