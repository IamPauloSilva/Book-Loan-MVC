using System.ComponentModel.DataAnnotations;

namespace BookLoanApp.Models
{
    public class BooksModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please fill the name title!")]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Cape { get; set; } = string.Empty;
        [Required]
        public string ISBN { get; set; } = string.Empty;
        [Required]
        public string Author { get; set; } = string.Empty;
        [Required]
        public string Genre { get; set; } = string.Empty;
        [Required]
        public int PublicationYear { get; set; }
        [Required]
        public int StockAmount { get; set; }

        public List<LoanModel> Loans { get; set; }

        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public DateTime LastAlterationDate { get; set; } = DateTime.Now;
    }
}
