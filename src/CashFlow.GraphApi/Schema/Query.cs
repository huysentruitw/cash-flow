using System;
using System.Threading.Tasks;
using AutoMapper;
using CashFlow.Query.Abstractions.Repositories;
using HotChocolate;

namespace CashFlow.GraphApi.Schema
{
    public sealed class Query
    {
        private static IMapper Mapper;

        public Query(OutputTypesMapperResolver mapperResolver)
        {
            Mapper = mapperResolver();
        }

        public async Task<Account[]> Accounts([Service] IAccountRepository repository)
            => Mapper.Map<Account[]>(await repository.GetAccounts());

        public async Task<string[]> ActiveCodeNames([Service] ICodeRepository repository)
            => await repository.GetActiveCodeNames();

        public async Task<CodeBalance[]> CodeBalances([Service] ICodeBalanceRepository repository, Guid? financialYearId)
            => Mapper.Map<CodeBalance[]>(await repository.GetCodeBalances(financialYearId));

        public async Task<Transaction[]> CodeTransactions([Service] ICodeBalanceRepository repository, Guid? financialYearId, [GraphQLNonNullType] string codeName)
            => Mapper.Map<Transaction[]>(await repository.GetCodeTransactions(financialYearId, codeName));

        public async Task<Code[]> Codes([Service] ICodeRepository repository)
            => Mapper.Map<Code[]>(await repository.GetCodes());

        public async Task<FinancialYear[]> FinancialYears([Service] IFinancialYearRepository repository)
            => Mapper.Map<FinancialYear[]>(await repository.GetFinancialYears());

        public async Task<StartingBalance[]> StartingBalances([Service] IFinancialYearRepository repository, Guid financialYearId)
            => Mapper.Map<StartingBalance[]>(await repository.GetFinancialYearStartingBalances(financialYearId));

        public async Task<string> SuggestEvidenceNumberForTransaction([Service] ITransactionRepository repository, Guid transactionId)
            => await repository.SuggestEvidenceNumberForTransaction(transactionId);

        public async Task<Supplier[]> Suppliers([Service] ISupplierRepository repository)
            => Mapper.Map<Supplier[]>(await repository.GetSuppliers());

        public async Task<Transaction[]> Transactions([Service] ITransactionRepository repository, Guid financialYearId)
            => Mapper.Map<Transaction[]>(await repository.GetTransactions(financialYearId));
    }
}
