using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Criptography;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.User.Register
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IMapper _mapper;
        private readonly IPasswordEncrypter _passwordEncrypter;
        private readonly IUsersReadOnlyRepository _usersReadOnlyRepository;
        public CreateUserUseCase(IMapper mapper,
            IPasswordEncrypter passwordEncrypter,
            IUsersReadOnlyRepository userReadOnly)
        {
            _mapper = mapper;
            _passwordEncrypter = passwordEncrypter;
        }
        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);
            var user = _mapper.Map<Domain.Entities.User>(request);
            var password = _passwordEncrypter.Encrypt(request.Password);

            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
            };
        }

        public async Task Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();
            var response = validator.Validate(request);
            var emailExist = await _usersReadOnlyRepository.ExistsEmail(request.Email);

            if (emailExist)
            {
                response.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessage.EMAIL_EXIST));
            }

            if (response.IsValid == false)
            {
                var errorMessages = response.Errors.Select(f => f.ErrorMessage).ToList();
                throw new ErrorOnValidateException(errorMessages);
            }
        }
    }
}
