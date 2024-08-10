using BookLoanApp.Enums;
using BookLoanApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BookLoanApp.Dto.User
{
    public class UserCreationDto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter a Full Name!")]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Enter an user Name!")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Enter a email!")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Enter a situation!")]
        public bool Situation { get; set; }
        [Required(ErrorMessage = "Enter a password!"),MinLength(6,ErrorMessage ="6 characters minimum")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password!"), Compare("Password",ErrorMessage ="Password does not match!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Choose a profile")]
        public ProfilesEnum Profile { get; set; }

        [Required(ErrorMessage = "Choose a profile")]
        public TurnoEnum Turno { get; set; }

        [Required(ErrorMessage = "Enter a street adress!")]
        public string StreetAdress { get; set; } = string.Empty;
        [Required(ErrorMessage = "Enter a city!")]
        public string City { get; set; } = string.Empty;
        [Required(ErrorMessage = "Enter a state!")]
        public string State { get; set; } = string.Empty;
        [Required(ErrorMessage = "Enter a country!")]
        public string Country { get; set; } = string.Empty;
        [Required(ErrorMessage = "Enter a door number!")]
        public int DoorNumber { get; set; }
        [Required(ErrorMessage = "Enter a zip code!")]
        public string Zipcode { get; set; } = string.Empty;
    }
}
