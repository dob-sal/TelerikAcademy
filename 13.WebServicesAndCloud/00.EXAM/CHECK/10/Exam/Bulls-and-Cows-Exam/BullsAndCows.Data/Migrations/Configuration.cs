namespace BullsAndCows.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using BullsAndCows.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<BullsAndCowsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BullsAndCows.Data.BullsAndCowsDbContext context)
        {
            //for testing scores service
            if (context.Users.Count() < 10)
            {
                for (int i = 0; i < 100; i++)
                {
                    context.Users.Add(new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = string.Format("user{0}", i),
                        Rank = i * 10
                    });
                }
            }
        }
    }
}
