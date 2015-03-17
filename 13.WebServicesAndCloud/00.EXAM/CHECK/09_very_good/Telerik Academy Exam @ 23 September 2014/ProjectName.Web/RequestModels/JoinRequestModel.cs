using System.ComponentModel.DataAnnotations;
namespace BullsAndCows.Web.RequestModels
{
    public class JoinRequestModel
    {
        [Required]
        public string Number { get; set; }
    }
}