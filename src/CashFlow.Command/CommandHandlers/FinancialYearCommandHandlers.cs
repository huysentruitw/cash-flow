using System.Threading.Tasks;
using CashFlow.Command.Abstractions;
using CashFlow.Command.Repositories;
using FluentValidation;
using MediatR;

namespace CashFlow.Command.CommandHandlers
{
    internal sealed class AddFinancialYearCommandHandler : SafeCommandHandler<AddFinancialYearCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IFinancialYearRepository _financialYearRepository;

        public AddFinancialYearCommandHandler(
            IAccountRepository accountRepository,
            IFinancialYearRepository financialYearRepository)
        {
            _accountRepository = accountRepository;
            _financialYearRepository = financialYearRepository;
        }

        protected override void DefineRules()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }

        protected override async Task<Unit> HandleValidatedCommand(AddFinancialYearCommand command)
        {
            if (command.PreviousFinancialYearId.HasValue)
            {
                await _accountRepository.SetupAccountBalancesForNewFinancialYear(
                    closingFinancialYear: command.PreviousFinancialYearId.Value,
                    newFinancialYearId: command.Id);
            }

            await _financialYearRepository.AddFinancialYear(command.Id, command.Name);
            return Unit.Value;
        }
    }

    internal sealed class ActivateFinancialYearCommandHandler : SafeCommandHandler<ActivateFinancialYearCommand>
    {
        private readonly IFinancialYearRepository _repository;

        public ActivateFinancialYearCommandHandler(IFinancialYearRepository repository)
        {
            _repository = repository;
        }

        protected override void DefineRules()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

        protected override async Task<Unit> HandleValidatedCommand(ActivateFinancialYearCommand command)
        {
            await _repository.ActivateFinancialYear(command.Id);
            return Unit.Value;
        }
    }
}
