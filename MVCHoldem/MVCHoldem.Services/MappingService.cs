namespace MVCHoldem.Services
{
    using AutoMapper;
    using Bytes2you.Validation;
    using MVCHoldem.Services.Contracts;

    public class MappingService : IMappingService
    {
        public static IMappingService Provider { get; set; } = new MappingService();

        public T Map<T>(object from)
        {
            Guard.WhenArgument(from, "Object to map").IsNull().Throw();
            return Mapper.Map<T>(from);
        }
    }
}
