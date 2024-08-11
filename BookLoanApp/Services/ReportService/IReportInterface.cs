using System.Data;

namespace BookLoanApp.Services.ReportService
{
    public interface IReportInterface
    {

        DataTable GetData<T> (List<T> list, int id);
    }
}
