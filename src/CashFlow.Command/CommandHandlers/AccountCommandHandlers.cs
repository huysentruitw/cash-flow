using System.Threading.Tasks;
using CashFlow.Command.Abstractions;
using CashFlow.Command.Repositories;
using FluentValidation;
using MediatR;

namespace CashFlow.Command.CommandHandlers
{
    internal sealed class AddAccountCommandHandler : SafeCommandHandler<AddAccountCommand>
    {
        private readonly IAccountRepository _repository;

        public AddAccountCommandHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        protected override void DefineRules()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
        }

        protected override async Task<Unit> HandleValidatedCommand(AddAccountCommand command)
        {
            await _repository.AddAccount(
                id: command.Id,
                name: command.Name,
                type: command.Type);
            return Unit.Value;
        }
    }

    internal sealed class RenameAccountCommandHandler : SafeCommandHandler<RenameAccountCommand>
    {
        private readonly IAccountRepository _repository;

        public RenameAccountCommandHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        protected override void DefineRules()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
        }

        protected override async Task<Unit> HandleValidatedCommand(RenameAccountCommand command)
        {
            await _repository.RenameAccount(
                id: command.Id,
                name: command.Name);
            return Unit.Value;
        }
    }

    internal sealed class ChangeAccountTypeCommandHandler : SafeCommandHandler<ChangeAccountTypeCommand>
    {
        private readonly IAccountRepository _repository;

        public ChangeAccountTypeCommandHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        protected override void DefineRules()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

        protected override async Task<Unit> HandleValidatedCommand(ChangeAccountTypeCommand command)
        {
            await _repository.ChangeAccountType(
                id: command.Id,
                type: command.Type);
            return Unit.Value;
        }
    }

    internal sealed class RemoveAccountCommandHandler : SafeCommandHandler<RemoveAccountCommand>
    {
        private readonly IAccountRepository _repository;

        public RemoveAccountCommandHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        protected override void DefineRules()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

        protected override async Task<Unit> HandleValidatedCommand(RemoveAccountCommand command)
        {
            await _repository.RemoveAccount(id: command.Id);
            return Unit.Value;
        }
    }
}
