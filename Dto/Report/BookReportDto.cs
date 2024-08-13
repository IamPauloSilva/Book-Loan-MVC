using BookLoanApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BookLoanApp.Dto.Report
{
    public class BookReportDto
    {

       
        public int? Id { get; set; }
        
        public string? Title { get; set; } = string.Empty;
     
        public string? Description { get; set; } = string.Empty;
        
        public string? Cape { get; set; } = string.Empty;
        
        public string? ISBN { get; set; } = string.Empty;
       
        public string? Author { get; set; } = string.Empty;
        
        public string? Genre { get; set; } = string.Empty;
        
        public int? PublicationYear { get; set; }
       
        public int? StockAmount { get; set; }

        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public DateTime LastAlterationDate { get; set; } = DateTime.Now;
    }
}
