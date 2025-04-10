using System.Net.Mime;
using CashFlow.Application.UseCases.Reports.Excel;
using CashFlow.Application.UseCases.Reports.Pdf;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;
    [Route("api/[controller]")]
    [ApiController]
public class ReportController : Controller
{
    [HttpGet("Excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel(
        [FromHeader] DateOnly month,
        [FromServices] IGetReportExcelUseCase useCase)
    {
        byte[] file = await useCase.Execute(month);

        if(file.Length > 0)
            return File(file,MediaTypeNames.Application.Octet,"Expenses.xlsx");

        return NoContent();
    }

    [HttpGet("Pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<IActionResult> GetPdf(
        [FromQuery] DateOnly month,
        [FromServices] IGetReportPdfUseCase useCase)
    {
        byte[] file = await useCase.Execute(month);
        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, "Expenses.pdf");
        return NoContent();
    }
}
