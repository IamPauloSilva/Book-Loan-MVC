using AutoMapper;
using BookLoanApp.Dto.Report;
using BookLoanApp.Enums;
using Newtonsoft.Json;
using System.Data;

namespace BookLoanApp.Services.ReportService
{
    public class ReportService : IReportInterface
    {
        private readonly IMapper _mapper;

        public ReportService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public DataTable GetData<T>(List<T> list, int id)
        {
            DataTable dt = new DataTable();

            dt.TableName = Enum.GetName(typeof(ReportEnum), id);

            var columns = list.First().GetType().GetProperties();

            foreach (var column in columns)
            {
                dt.Columns.Add(column.Name, column.PropertyType);

            }

            switch (id)
            {
                case 1:
                    var bookData = JsonConvert.SerializeObject(list);
                    var bookModelData = JsonConvert.DeserializeObject<List<BookReportDto>>(bookData);

                    if (bookModelData != null)
                    {
                        return ExportBook(dt, bookModelData);
                    }
                break;

                case 2:
                    var clientData = JsonConvert.SerializeObject(list);
                    var clientModelData = JsonConvert.DeserializeObject<List<UserReportDto>>(clientData);
                    if (clientModelData != null)
                    {
                        return ExportUser(dt, clientModelData);
                    }

                    break;
                case 3:
                    var employeeData = JsonConvert.SerializeObject(list);
                    var employeeModelData = JsonConvert.DeserializeObject<List<UserReportDto>>(employeeData);
                    if (employeeModelData != null)
                    {
                        return ExportUser(dt, employeeModelData);
                    }

                    break;

                case 4:
                    var loanData = JsonConvert.SerializeObject(list);
                    var loanModelData = JsonConvert.DeserializeObject<List<LoanReportDto>>(loanData);
                    if (loanModelData != null)
                    {
                        return ExportLoan(dt, loanModelData);
                    }

                    break;

                case 5:
                    var pendingLoansData = JsonConvert.SerializeObject(list);
                    var pendingLoansModelData = JsonConvert.DeserializeObject<List<LoanReportDto>>(pendingLoansData);
                    if (pendingLoansModelData != null)
                    {
                        return ExportPendingLoans(dt, pendingLoansModelData);
                    }

                    break;
            }

            return new DataTable();
        }


        public DataTable ExportBook(DataTable dt, List<BookReportDto> datalist)
        {
            foreach (var item in datalist)
            {
                dt.Rows.Add(item.Id, item.Title, item.Description, item.Cape, item.ISBN, item.Author, item.Genre, item.PublicationYear, item.StockAmount, item.RegisterDate, item.LastAlterationDate);
            }
            return dt;
        }

        public DataTable ExportUser(DataTable dt, List<UserReportDto> datalist)
        {
            foreach (var item in datalist)
            {
                dt.Rows.Add(item.Id, item.FullName, item.UserName, item.Email, item.Situation == "True" ? "Active" : "Inactive", item.RegisterDate, item.LastAlterationDate, item.Profile, item.Turno,
                    item.StreetAdress, item.City, item.State, item.Country, item.DoorNumber,item.Zipcode);
            }
            return dt;
        }

        public DataTable ExportLoan(DataTable dt, List<LoanReportDto> datalist)
        {
            foreach (var item in datalist)
            {
                dt.Rows.Add(item.Id, item.UserId, item.UserName, item.BookId, item.FullName, item.ISBN, item.Title, item.LoanDate, item.DeliverDate);
            }
            return dt;
        }

        public DataTable ExportPendingLoans(DataTable dt, List<LoanReportDto> datalist)
        {
            foreach (var item in datalist)
            {
                dt.Rows.Add(item.Id, item.UserId, item.UserName, item.BookId, item.FullName, item.ISBN, item.Title, item.LoanDate);
            }
            return dt;
        }
    }
}
