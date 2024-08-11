using BookLoanApp.Models;

namespace BookLoanApp.Dto.Report
{
    public class LoanReportDto
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        
        public string UserName { get; set; }
        public int BookId { get; set; }
        
        public string FullName {  get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public DateTime LoanDate { get; set; } = DateTime.Now;
        public DateTime? DeliverDate { get; set; }
    }
}
