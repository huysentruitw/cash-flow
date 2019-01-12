using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace CashFlow.Command
{
    internal abstract class SafeCommandHandler<TCommand, TResult>
        : AbstractValidator<TCommand>
            , IRequestHandler<TCommand, TResult>
        where TCommand : IRequest<TResult>
    {
        public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
        {
            DefineRules();
            this.ValidateAndThrow(command);
            return await HandleValidatedCommand(command);
        }

        protected abstract void DefineRules();

        protected abstract Task<TResult> HandleValidatedCommand(TCommand command);
    }

    internal abstract class SafeCommandHandler<TCommand> : SafeCommandHandler<TCommand, Unit>
        where TCommand : IRequest
    {
    }
}
