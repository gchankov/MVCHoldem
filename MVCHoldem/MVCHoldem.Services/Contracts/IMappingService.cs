namespace MVCHoldem.Services.Contracts
{
    public interface IMappingService
    {
        T Map<T>(object from);
    }
}