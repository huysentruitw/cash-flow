using AutoMapper;

namespace CashFlow.GraphApi
{
    /// <summary>
    /// Decorator for resolving the mapper for OutputTypes.
    /// </summary>
    public delegate IMapper OutputTypesMapperResolver();
}
