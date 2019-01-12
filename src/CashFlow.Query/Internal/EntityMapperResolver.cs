using AutoMapper;

namespace CashFlow.Query
{
    /// <summary>
    /// Decorator for resolving the entity mapper.
    /// </summary>
    internal delegate IMapper EntityMapperResolver();
}
