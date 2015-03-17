namespace ServicesExam.Data
{
    using ServicesExam.Data.Migrations;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ServicesExam.Model;
    using System;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ServicesExamConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

       public IDbSet<Game> Games { get; set; }
       public IDbSet<Guess> Guesses { get; set; }

       public IDbSet<Message> Messages { get; set; }
    }
}
