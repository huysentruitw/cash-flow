using System.Threading.Tasks;
using CashFlow.Command.Abstractions;
using CashFlow.Command.Repositories;
using FluentValidation;
using MediatR;

namespace CashFlow.Command.CommandHandlers
{
    internal sealed class AddSupplierCommandHandler : SafeCommandHandler<AddSupplierCommand>
    {
        private readonly ISupplierRepository _repository;

        public AddSupplierCommandHandler(ISupplierRepository repository)
        {
            _repository = repository;
        }

        protected override void DefineRules()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
        }

        protected override async Task<Unit> HandleValidatedCommand(AddSupplierCommand command)
        {
            await _repository.AddSupplier(
                id: command.Id,
                name: command.Name,
                contactInfo: command.ContactInfo);
            return Unit.Value;
        }
    }

    internal sealed class RenameSupplierCommandHandler : SafeCommandHandler<RenameSupplierCommand>
    {
        private readonly ISupplierRepository _repository;

        public RenameSupplierCommandHandler(ISupplierRepository repository)
        {
            _repository = repository;
        }

        protected override void DefineRules()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
        }

        protected override async Task<Unit> HandleValidatedCommand(RenameSupplierCommand command)
        {
            await _repository.RenameSupplier(
                id: command.Id,
                name: command.Name);
            return Unit.Value;
        }
    }

    internal sealed class UpdateSupplierContactInfoCommandHandler : SafeCommandHandler<UpdateSupplierContactInfoCommand>
    {
        private readonly ISupplierRepository _repository;

        public UpdateSupplierContactInfoCommandHandler(ISupplierRepository repository)
        {
            _repository = repository;
        }

        protected override void DefineRules()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

        protected override async Task<Unit> HandleValidatedCommand(UpdateSupplierContactInfoCommand command)
        {
            await _repository.UpdateSupplierContactInfo(
                id: command.Id,
                contactInfo: command.ContactInfo);
            return Unit.Value;
        }
    }

    internal sealed class RemoveSupplierCommandHandler : SafeCommandHandler<RemoveSupplierCommand>
    {
        private readonly ISupplierRepository _repository;

        public RemoveSupplierCommandHandler(ISupplierRepository repository)
        {
            _repository = repository;
        }

        protected override void DefineRules()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

        protected override async Task<Unit> HandleValidatedCommand(RemoveSupplierCommand command)
        {
            await _repository.RemoveSupplier(id: command.Id);
            return Unit.Value;
        }
    }
}
