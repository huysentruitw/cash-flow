using System;
using CashFlow.Command.Abstractions;

namespace CashFlow.GraphApi.Schema
{
    public class MutationInfo
    {
        public Guid CorrelationId { get; set; }

        public static MutationInfo FromCommand<TResult>(Command<TResult> command)
            => new MutationInfo
            {
                CorrelationId = command.Headers.CorrelationId
            };
    }

    public class MutationInfo<TMutationResult>
    {
        public Guid CorrelationId { get; set; }

        public TMutationResult Result { get; set; }

        public static MutationInfo<TMutationResult> FromCommand<TResult>(Command<TResult> command, TMutationResult result)
            => new MutationInfo<TMutationResult>
            {
                CorrelationId = command.Headers.CorrelationId,
                Result = result
            };
    }
}
