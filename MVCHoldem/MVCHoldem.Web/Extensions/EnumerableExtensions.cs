namespace MVCHoldem.Web.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using MVCHoldem.Services;

    public static class EnumerableExtensions
    {
        public static IEnumerable<ToType> Map<FromType, ToType>(this IEnumerable<FromType> source)
        {
            return source.Select(x => MappingService.Provider.Map<ToType>(x));
        }
    }
}