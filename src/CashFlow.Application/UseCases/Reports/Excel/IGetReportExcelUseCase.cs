namespace CashFlow.Application.UseCases.Reports.Excel
{
    public interface IGetReportExcelUseCase
    {
        public async Task<byte[]> Execute(DateOnly month);
    }
}
