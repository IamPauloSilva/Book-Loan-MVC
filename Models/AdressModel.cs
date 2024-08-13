using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookLoanApp.Models
{
    public class AdressModel
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public string? StreetAdress { get; set; } = string.Empty;
        [Required]
        public string? City { get; set; } = string.Empty;
        [Required]
        public string? State { get; set; } = string.Empty;
        [Required]
        public string? Country { get; set; } = string.Empty;
        [Required]
        public int? DoorNumber { get; set; }
        [Required]
        public string? Zipcode { get; set; } = string.Empty;

        [JsonIgnore]
        public UserModel? User { get; set; }
        public int? UserId { get; set; }

    }
}
