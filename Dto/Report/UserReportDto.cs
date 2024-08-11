using BookLoanApp.Enums;
using BookLoanApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BookLoanApp.Dto.Report
{
    public class UserReportDto
    {

        public int Id { get; set; }


        public string FullName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Situation { get; set; }

        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public DateTime LastAlterationDate { get; set; } = DateTime.Now;

        public string Profile { get; set; }

        public string Turno { get; set; }

        public string StreetAdress { get; set; } = string.Empty;
        
        public string City { get; set; } = string.Empty;
        
        public string State { get; set; } = string.Empty;
        
        public string Country { get; set; } = string.Empty;
        
        public int DoorNumber { get; set; }
        
        public string Zipcode { get; set; } = string.Empty;
    }

       
}
