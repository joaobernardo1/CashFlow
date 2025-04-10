namespace CashFlow.Application.UseCases.Reports.Pdf
{
    public interface IGetReportPdfUseCase
    {
        Task<byte[]> Execute(DateOnly month);   
    }
}
