using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        public void RequestToEntity()
        {
            CreateMap<RequestExpenseJson, Expense>();
        }

        public void EntityToResponse()
        {
            CreateMap<Expense, ResponseRegisterExpenseJson>();
            CreateMap<Expense, ResponseShortExpensesJson>();
            CreateMap<Expense, ResponseExpenseJson>();
        }
    }
}
