namespace BullsAndCows.Web.DataModels
{
    using System.ComponentModel.DataAnnotations;

    public class PlayRequestDataModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }
    }
}