namespace Application.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Application.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<DbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DbContext context)
        {
         /*  if (context.Games.Any())
            {
                return;
            }

            var game = new Game
                              {
                                  Name = "Some Random Title",
                                  BluePlayerId = "No blue player yet",
                                  RedPlayerId = "doncho@minkov.it",
                                  GameState = Models.GameState.WaitingForOpponent,
                                  DateCreated = DateTime.Now
                              };

            context.Games.Add(game);
            context.SaveChanges();  */
        }
    }
}