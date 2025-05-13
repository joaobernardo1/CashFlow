using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.User.Register
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IMapper _mapper;
        public CreateUserUseCase(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            Validate(request);
            var user = _mapper.Map<Domain.Entities.User>(request);
            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
            };
        }

        public void Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();
            var response = validator.Validate(request);

            if (response.IsValid == false)
            {
                var errorMessages = response.Errors.Select(f => f.ErrorMessage).ToList();
                throw new ErrorOnValidateException(errorMessages);
            }
        }
    }
}
