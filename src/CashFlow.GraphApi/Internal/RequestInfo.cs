using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace CashFlow.GraphApi
{
    public interface IRequestInfo
    {
        ClaimsIdentity Identity { get; }
        IPAddress IpAddress { get; }
    }

    public sealed class RequestInfo : IRequestInfo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestInfo(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsIdentity Identity => _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;

        public IPAddress IpAddress => _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress;
    }
}
