using System;
using System.Threading.Tasks;
using AutoMapper;
using CashFlow.Query.Abstractions.Repositories;
using GraphQL.Conventions;

namespace CashFlow.GraphApi.Schema
{
    internal sealed class Query
    {
        private static IMapper _mapper;

        public Query(OutputTypesMapperResolver mapperResolver)
        {
            _mapper = mapperResolver();
        }

        public async Task<Account[]> Accounts([Inject] IAccountRepository repository)
            => _mapper.Map<Account[]>(await repository.GetAccounts());

        public async Task<Code[]> Codes([Inject] ICodeRepository repository)
            => _mapper.Map<Code[]>(await repository.GetCodes());

        public async Task<FinancialYear[]> FinancialYears([Inject] IFinancialYearRepository repository)
            => _mapper.Map<FinancialYear[]>(await repository.GetFinancialYears());

        public async Task<Supplier[]> Suppliers([Inject] ISupplierRepository repository)
            => _mapper.Map<Supplier[]>(await repository.GetSuppliers());

        public async Task<Transaction[]> Transactions([Inject] ITransactionRepository repository, Guid financialYearId)
            => _mapper.Map<Transaction[]>(await repository.GetTransactions(financialYearId));
    }
}
