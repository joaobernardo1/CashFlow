using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll
{
    public class GetAllExpensesUseCase : IGetAllExpensesUseCase
    {
        public readonly IExpensesRepository _repository;
        public readonly IMapper _mapper;

        public GetAllExpensesUseCase(IExpensesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseExpenseJson> Execute()
        {
            var response = await _repository.GetAll();
            return new ResponseExpenseJson
            {
                Expenses = _mapper.Map<List<ResponseShortExpensesJson>>(response)
            };
        }
    }
}
