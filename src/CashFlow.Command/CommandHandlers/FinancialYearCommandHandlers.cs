using System.Threading.Tasks;
using CashFlow.Command.Abstractions;
using CashFlow.Command.Repositories;
using FluentValidation;
using MediatR;

namespace CashFlow.Command.CommandHandlers
{
    internal sealed class AddFinancialYearCommandHandler : SafeCommandHandler<AddFinancialYearCommand>
    {
        private readonly IFinancialYearRepository _repository;

        public AddFinancialYearCommandHandler(IFinancialYearRepository repository)
        {
            _repository = repository;
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
                // TODO Get end balance of the previous financial year, for each account, and use it as starting balance
            }

            await _repository.AddFinancialYear(command.Id, command.Name);
            return Unit.Value;
        }
    }
}
