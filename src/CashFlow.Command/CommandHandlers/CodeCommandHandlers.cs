using System.Threading.Tasks;
using CashFlow.Command.Abstractions;
using CashFlow.Command.Repositories;
using FluentValidation;
using MediatR;

namespace CashFlow.Command.CommandHandlers
{
    internal sealed class AddCodeCommandHandler : SafeCommandHandler<AddCodeCommand>
    {
        private readonly ICodeRepository _repository;

        public AddCodeCommandHandler(ICodeRepository repository)
        {
            _repository = repository;
        }

        protected override void DefineRules()
        {
            RuleFor(x => x.Name).NotNull().MaximumLength(100);
        }

        protected override async Task<Unit> HandleValidatedCommand(AddCodeCommand command)
        {
            await _repository.AddCode(command.Name);
            return Unit.Value;
        }
    }

    internal sealed class RenameCodeCommandHandler : SafeCommandHandler<RenameCodeCommand>
    {
        private readonly ICodeRepository _repository;

        public RenameCodeCommandHandler(ICodeRepository repository)
        {
            _repository = repository;
        }

        protected override void DefineRules()
        {
            RuleFor(x => x.OriginalName).NotNull().MaximumLength(100);
            RuleFor(x => x.NewName).NotNull().MaximumLength(100);
            RuleFor(x => x.OriginalName).NotEqual(x => x.NewName);
        }

        protected override async Task<Unit> HandleValidatedCommand(RenameCodeCommand command)
        {
            await _repository.RenameCode(originalName: command.OriginalName, newName: command.NewName);
            return Unit.Value;
        }
    }

    internal sealed class RemoveCodeCommandHandler : SafeCommandHandler<RemoveCodeCommand>
    {
        private readonly ICodeRepository _repository;

        public RemoveCodeCommandHandler(ICodeRepository repository)
        {
            _repository = repository;
        }

        protected override void DefineRules()
        {
            RuleFor(x => x.Name).NotNull().MaximumLength(100);
        }

        protected override async Task<Unit> HandleValidatedCommand(RemoveCodeCommand command)
        {
            await _repository.RemoveCode(command.Name);
            return Unit.Value;
        }
    }
}
