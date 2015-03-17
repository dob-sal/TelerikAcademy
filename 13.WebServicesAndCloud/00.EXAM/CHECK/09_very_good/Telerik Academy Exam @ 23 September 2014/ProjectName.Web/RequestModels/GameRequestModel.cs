namespace BullsAndCows.Web.RequestModels
{
    using System.ComponentModel.DataAnnotations;

    public class GameRequestModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }
    }
}