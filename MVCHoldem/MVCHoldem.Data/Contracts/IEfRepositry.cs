namespace MVCHoldem.Data.Contracts
{
    using System.Linq;

    public interface IEfRepository<T>
        where T : class
    {
        IQueryable<T> All { get; }

        IQueryable<T> AllIncludingDeleted { get; }

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}
