using BookLoanApp.Dto.Adress;
using BookLoanApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookLoanApp.Dto.User
{
    public class UserEditDto
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Enter a Full Name!")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "Enter a Username!")]
        public string? UserName {  get; set; }
        [Required(ErrorMessage = "Enter a Email adress!")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Select a profile!")]
        public ProfilesEnum Profile { get; set; }
        [Required(ErrorMessage = "Select a Shift!")]
        public TurnoEnum Turno { get; set; }
        public AdressEditDto? Adress { get; set; }

    }
}
