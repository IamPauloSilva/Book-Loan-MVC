using AutoMapper;
using BookLoanApp.Dto;
using BookLoanApp.Models;

namespace BookLoanApp.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper() 
        {
            CreateMap<BookCreationDto, BooksModel>();
        }
    }
}
