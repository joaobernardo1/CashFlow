using CashFlow.Application.UseCases.User.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(
            [FromBody]  RequestRegisterUserJson request,
            [FromServices] ICreateUserUseCase useCase)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty,response);
        }
    }
}
