using BookLoanApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookLoanApp.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public bool Situation { get; set; }
        [Required]
        public byte[] HashPass { get; set; }
        [Required]
        public byte[] SaltPass { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public DateTime LastAlterationDate { get; set; } = DateTime.Now;

        [Required]
        public ProfilesEnum Profile { get; set; }
        [Required]
        public TurnoEnum Turno { get; set; }

        [Required]
        public AdressModel Adress { get; set; } 
    }

        
}
