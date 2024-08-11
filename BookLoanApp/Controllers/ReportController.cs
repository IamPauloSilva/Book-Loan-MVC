using AutoMapper;
using BookLoanApp.Dto.Report;
using BookLoanApp.Models;
using BookLoanApp.Services.BookService;
using BookLoanApp.Services.LoanService;
using BookLoanApp.Services.ReportService;
using BookLoanApp.Services.SessionService;
using BookLoanApp.Services.UserService;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookLoanApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly ISessionInterface _sessionInterface; 
        private readonly IBookInterface _bookInterface;
        private readonly IUserInterface _userInterface;
        private readonly ILoanInterface _loanInterface;
        private readonly IReportInterface _reportInterface;
        private readonly IMapper _mapper;

        public ReportController(ISessionInterface sessionInterface, IBookInterface bookInterface,
            IUserInterface userInterface, ILoanInterface loanInterface, IReportInterface reportInterface,IMapper mapper)
        {
            _sessionInterface = sessionInterface;
            _bookInterface = bookInterface;
            _userInterface = userInterface;
            _loanInterface = loanInterface;
            _reportInterface = reportInterface;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Generate(int id)
        {
            var table = new DataTable();
            switch (id)
            {
                case 1: 
                    List<BooksModel> books = await _bookInterface.GetBooks();
                    List<BookReportDto> booksData = _mapper.Map<List<BookReportDto>>(books);

                    if (books.Count == 0)
                    {
                        TempData["MensagemErro"] = "Theres no data for this report";
                        return RedirectToAction("Index", "Report");
                    }
                        table = _reportInterface.GetData(booksData, id);
                break;

                case 2:
                    List<UserModel> clients = await _userInterface.GetUsers(0);
                    List<UserReportDto> clientData = new List<UserReportDto>();
                    if (clients.Count > 0)
                    {
                        foreach (var client in clients)
                        {
                            clientData.Add(
                                new UserReportDto
                                {
                                    Id = client.Id,
                                    FullName = client.FullName,
                                    UserName = client.UserName,
                                    Email = client.Email,
                                    Situation = client.Situation.ToString(),
                                    Profile = client.Profile.ToString(),
                                    Turno = client.Turno.ToString(),
                                    StreetAdress = client.Adress.StreetAdress,
                                    DoorNumber = client.Adress.DoorNumber,
                                    City = client.Adress.City,
                                    State = client.Adress.State,
                                    Country = client.Adress.Country,
                                    Zipcode = client.Adress.Zipcode,
                                    RegisterDate = client.RegisterDate,
                                    LastAlterationDate = client.LastAlterationDate,


                                }
                             );

                        }
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Theres no data for this report";
                        return RedirectToAction("Index", "Report");
                    }

                    table = _reportInterface.GetData(clientData, id);

                    
                    break;

                case 3:
                    List<UserModel> employees = await _userInterface.GetUsers(null);
                    List<UserReportDto> empolyeeData = new List<UserReportDto>();
                    if (employees.Count > 0)
                    {
                        foreach (var employee in employees)
                        {
                            empolyeeData.Add(
                                new UserReportDto
                                {
                                    Id = employee.Id,
                                    FullName = employee.FullName,
                                    UserName = employee.UserName,
                                    Email = employee.Email,
                                    Situation = employee.Situation.ToString(),
                                    Profile = employee.Profile.ToString(),
                                    Turno = employee.Turno.ToString(),
                                    StreetAdress = employee.Adress.StreetAdress,
                                    DoorNumber = employee.Adress.DoorNumber,
                                    City = employee.Adress.City,
                                    State = employee.Adress.State,
                                    Country = employee.Adress.Country,
                                    Zipcode = employee.Adress.Zipcode,
                                    RegisterDate = employee.RegisterDate,
                                    LastAlterationDate = employee.LastAlterationDate,


                                }
                             );

                        }
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Theres no data for this report";
                        return RedirectToAction("Index", "Report");
                    }

                    table = _reportInterface.GetData(empolyeeData, id);


                    break;

                case 4:
                    List<LoanModel> loans = await _loanInterface.GetAllLoans(null);
                    List<LoanReportDto> loansData = new List<LoanReportDto>();
                    if (loans.Count > 0)
                    {
                        foreach (var loan in loans)
                        {
                            loansData.Add(
                                new LoanReportDto
                                {
                                    Id = loan.Id,
                                    UserId = loan.UserId,
                                    UserName = loan.User.UserName,
                                    BookId = loan.BookId,
                                    FullName = loan.User.FullName,
                                    ISBN = loan.Books.ISBN,
                                    Title = loan.Books.Title,
                                    LoanDate = loan.LoanDate,
                                    DeliverDate = (DateTime)loan.LoanDate,
                                }
                                );
                        }
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Theres no data for this report";
                        return RedirectToAction("Index", "Report");
                    }


                    table = _reportInterface.GetData(loansData, id);
                break;

                case 5:
                    List<LoanModel> loansPending = await _loanInterface.GetAllLoans("pending");
                    List<LoanReportDto> loansPendingData = new List<LoanReportDto>();

                    if (loansPending.Count > 0)
                    {
                        foreach (var loan in loansPending)
                        {
                            loansPendingData.Add(
                                new LoanReportDto
                                {
                                    Id = loan.Id,
                                    UserId = loan.UserId,
                                    UserName = loan.User.UserName,
                                    BookId = loan.BookId,
                                    FullName = loan.User.FullName,
                                    ISBN = loan.Books.ISBN,
                                    Title = loan.Books.Title,
                                    LoanDate = loan.LoanDate,
                                    
                                }
                                );
                        }
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Theres no data for this report";
                        return RedirectToAction("Index", "Report");
                    }
                    

                    table = _reportInterface.GetData(loansPendingData, id);
                    break;
            }

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(table, "Data");

                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Data.xml");
                }

            }
        }
    }
}
