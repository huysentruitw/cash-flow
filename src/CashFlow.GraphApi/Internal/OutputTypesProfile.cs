using AutoMapper;
using CashFlow.GraphApi.Schema;
using Entities = CashFlow.Data.Abstractions.Entities;
using Models = CashFlow.Query.Abstractions.Models;

namespace CashFlow.GraphApi
{
    internal sealed class OutputTypesProfile : Profile
    {
        public OutputTypesProfile()
        {
            CreateMap<Entities.Account, Account>();
            CreateMap<Entities.Code, Code>();
            CreateMap<Entities.FinancialYear, FinancialYear>();
            CreateMap<Entities.StartingBalance, StartingBalance>();
            CreateMap<Entities.Supplier, Supplier>();
            CreateMap<Entities.Transaction, Transaction>();
            CreateMap<Entities.TransactionCode, TransactionCode>();

            CreateMap<Models.CodeBalance, CodeBalance>();
        }
    }
}
