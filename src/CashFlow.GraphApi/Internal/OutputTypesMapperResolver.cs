using AutoMapper;

namespace CashFlow.GraphApi
{
    /// <summary>
    /// Decorator for resolving the mapper for OutputTypes.
    /// </summary>
    internal delegate IMapper OutputTypesMapperResolver();
}
