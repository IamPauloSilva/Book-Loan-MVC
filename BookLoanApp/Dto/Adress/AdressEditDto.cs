using System.ComponentModel.DataAnnotations;

namespace BookLoanApp.Dto.Adress
{
    public class AdressEditDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter a Street adress!")]
        public string StreetAdress { get; set; }
        [Required(ErrorMessage = "Enter a city!")]
        public string City { get; set; }
        [Required(ErrorMessage = "Enter a state!")]
        public string State { get; set; }
        [Required(ErrorMessage = "Enter a country!")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Enter a door number!")]
        public int DoorNumber { get; set; }
        [Required(ErrorMessage = "Enter a zip code!")]
        public string Zipcode { get; set; } 
        public int UserID {  get; set; }
    }
}
