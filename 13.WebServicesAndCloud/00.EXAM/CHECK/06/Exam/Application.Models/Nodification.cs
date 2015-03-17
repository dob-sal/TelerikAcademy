namespace Application.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Nodification
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string RedPlayeId { get; set; }

        [Required]
        public string BluelayeId { get; set; }
    }
}