using BullAndCows.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BullAndCows.Data.Migrations;
using BullAndCows.Data;

namespace BullAndCows.Data
{
    public class BullAndCowsDbContext : IdentityDbContext<ApplicationUser>
    {
        public BullAndCowsDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BullAndCowsDbContext, Configuration>());
        }

        public static BullAndCowsDbContext Create()
        {
            return new BullAndCowsDbContext();
        }

        public IDbSet<Game> Games { get; set; }

        public IDbSet<Guess> Guesses { get; set; }
        public IDbSet<Notification> Notifications { get; set; }

    }
}
