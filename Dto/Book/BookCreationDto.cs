using System.ComponentModel.DataAnnotations;

namespace BookLoanApp.Dto.Book
{
    public class BookCreationDto
    {
        [Required(ErrorMessage = "Please fill the name title!")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please fill the  Description!")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please fill the ISBN!")]
        public string ISBN { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please fill the Author!")]
        public string Author { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please fill the Genre!")]
        public string Genre { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please fill the Publication year!")]
        public int PublicationYear { get; set; }
        [Required(ErrorMessage = "Please fill the Stock amount!")]
        public int StockAmount { get; set; }
    }
}
