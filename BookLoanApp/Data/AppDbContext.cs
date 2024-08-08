using BookLoanApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLoanApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }
        public DbSet<BooksModel> Books { get; set; }
        public DbSet<UserModel> Users { get; set; }

        public DbSet<AdressModel> Adresses { get; set; }
    }
}
