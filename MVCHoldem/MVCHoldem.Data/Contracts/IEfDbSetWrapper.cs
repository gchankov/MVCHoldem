namespace MVCHoldem.Data.Contracts
{
    using System.Linq;

    public interface IEfDbSetWrapper<T>
        where T : class
    {
        IQueryable<T> AllWithoutDeleted { get; }

        IQueryable<T> AllIncludingDeleted { get; }

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}
