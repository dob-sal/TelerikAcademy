namespace BullsAndCows.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    
    public class GameUser : IdentityUser
    {
        private ICollection<Guess> guesses;
        private ICollection<Game> games;

        public GameUser()
        {
            this.guesses = new HashSet<Guess>();
            this.games = new HashSet<Game>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<GameUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        //[Required]
        [MinLength(4)]
        [MaxLength(4)]
        public string UserNumber { get; set; }

        public virtual ICollection<Guess> Guesses 
        {
            get { return this.guesses; }
            set { this.guesses = value; }
        }

        public virtual ICollection<Game> Games
        {
            get { return this.games; }
            set { this.games = value; }
        }

        public int WinsCount { get; set; }

        public int LossesCount { get; set; }

        public int Rank 
        {
            get { return 100 * this.WinsCount + 15 * this.LossesCount; } 
        }
    }
}
