using System;
using System.Threading.Tasks;
using AutoMapper;
using CashFlow.Query.Abstractions.Repositories;
using GraphQL.Conventions;

namespace CashFlow.GraphApi.Schema
{
    internal sealed class Query
    {
        private static IMapper Mapper;

        public Query(OutputTypesMapperResolver mapperResolver)
        {
            Mapper = mapperResolver();
        }

        public async Task<Account[]> Accounts([Inject] IAccountRepository repository)
            => Mapper.Map<Account[]>(await repository.GetAccounts());

        public async Task<CodeBalance[]> CodeBalances([Inject] ICodeBalanceRepository repository, Guid? financialYearId)
            => Mapper.Map<CodeBalance[]>(await repository.GetCodeBalances(financialYearId));

        public async Task<Transaction[]> CodeTransactions([Inject] ICodeBalanceRepository repository, Guid? financialYearId, NonNull<string> codeName)
            => Mapper.Map<Transaction[]>(await repository.GetCodeTransactions(financialYearId, codeName));

        public async Task<Code[]> Codes([Inject] ICodeRepository repository)
            => Mapper.Map<Code[]>(await repository.GetCodes());

        public async Task<FinancialYear[]> FinancialYears([Inject] IFinancialYearRepository repository)
            => Mapper.Map<FinancialYear[]>(await repository.GetFinancialYears());

        public async Task<StartingBalance[]> StartingBalances([Inject] IFinancialYearRepository repository, Guid financialYearId)
            => Mapper.Map<StartingBalance[]>(await repository.GetFinancialYearStartingBalances(financialYearId));

        public async Task<string> SuggestEvidenceNumberForTransaction([Inject] ITransactionRepository repository, Guid transactionId)
            => await repository.SuggestEvidenceNumberForTransaction(transactionId);

        public async Task<Supplier[]> Suppliers([Inject] ISupplierRepository repository)
            => Mapper.Map<Supplier[]>(await repository.GetSuppliers());

        public async Task<Transaction[]> Transactions([Inject] ITransactionRepository repository, Guid financialYearId)
            => Mapper.Map<Transaction[]>(await repository.GetTransactions(financialYearId));
    }
}
