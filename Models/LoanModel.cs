using System.Text.Json.Serialization;

namespace BookLoanApp.Models
{
    public class LoanModel
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        [JsonIgnore]
        public UserModel? User { get; set; }
        public int? BookId { get; set; }
        [JsonIgnore]
        public BooksModel? Books { get; set; }
        public DateTime LoanDate { get; set; } = DateTime.Now;
        public DateTime? DeliverDate { get; set; }
    }
}
