using System;
using System.Net;
using System.Security.Claims;
using MediatR;

namespace CashFlow.Command.Abstractions
{
    public abstract class Command : Command<Unit>, IRequest
    {
    }

    public abstract class Command<TResult> : IRequest<TResult>
    {
        public CommandHeaders Headers { get; set; }
    }

    public sealed class CommandHeaders
    {
        public CommandHeaders(Guid correlationId, ClaimsIdentity identity, IPAddress remoteIpAddress)
        {
            CorrelationId = correlationId;
            Identity = identity;
            RemoteIpAddress = remoteIpAddress;
        }

        public Guid CorrelationId { get; }

        public ClaimsIdentity Identity { get; }

        public IPAddress RemoteIpAddress { get; }
    }
}
