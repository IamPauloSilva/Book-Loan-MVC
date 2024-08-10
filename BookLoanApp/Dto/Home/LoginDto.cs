using System.ComponentModel.DataAnnotations;

namespace BookLoanApp.Dto.Home
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Fill Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Fill password")]
        public string Password { get; set; }
    }
}
