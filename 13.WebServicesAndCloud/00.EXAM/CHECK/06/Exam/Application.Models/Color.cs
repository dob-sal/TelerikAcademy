namespace Application.Models
{
    using System;

    public class Color
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string ColorType { get; set; }
    }
}