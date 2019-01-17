using AutoMapper;
using CashFlow.Query.Abstractions.Models;
using Entities = CashFlow.Persistence.Entities;

namespace CashFlow.Query
{
    internal sealed class EntityMapperProfile : Profile
    {
        public EntityMapperProfile()
        {
            CreateMap<Entities.Account, Account>();
            CreateMap<Entities.Code, Code>();
            CreateMap<Entities.FinancialYear, FinancialYear>();
            CreateMap<Entities.Supplier, Supplier>();
        }
    }
}
