using Microsoft.AspNet.Identity.EntityFramework;
using BullsAndCows.Data.Migrations;
using BullsAndCows.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Data
{
    public class BullsAndCowsDbContext : IdentityDbContext<User>, IBullsAndCowsContext
    {
        public BullsAndCowsDbContext()
            : base("ProjectNameConnectionString", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BullsAndCowsDbContext, Configuration>());
        }

        public IDbSet<Game> Games { get; set; }

        public IDbSet<Notification> Notifications { get; set; }

        public IDbSet<Guess> Guesses { get; set; }

        public IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }

        public static BullsAndCowsDbContext Create()
        {
            return new BullsAndCowsDbContext();
        }
    }
}
