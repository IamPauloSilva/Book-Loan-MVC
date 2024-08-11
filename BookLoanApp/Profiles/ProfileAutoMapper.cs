using AutoMapper;
using BookLoanApp.Dto.Adress;
using BookLoanApp.Dto.Book;
using BookLoanApp.Dto.Report;
using BookLoanApp.Models;

namespace BookLoanApp.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper() 
        {
            CreateMap<BookCreationDto, BooksModel>();
            CreateMap<BooksModel, BookEditDto>();
            CreateMap<BookEditDto, BooksModel>();
            CreateMap<AdressModel, AdressEditDto>();
            CreateMap<AdressEditDto, AdressModel>();
            CreateMap<BooksModel, BookReportDto>();
            CreateMap<UserModel, UserReportDto>();
            CreateMap<LoanModel, LoanReportDto>();
        }
    }
}
