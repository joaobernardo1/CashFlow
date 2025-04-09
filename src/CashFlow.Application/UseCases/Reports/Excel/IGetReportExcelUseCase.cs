namespace CashFlow.Application.UseCases.Reports.Excel
{
    public interface IGetReportExcelUseCase
    {
        Task<byte[]> Execute(DateOnly month);
    }
}
