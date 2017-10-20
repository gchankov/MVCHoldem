namespace MVCHoldem.Data.SetWrappers
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using MVCHoldem.Data.Contracts;
    using MVCHoldem.Data.Models.Contracts;

    public class EfDbSetWrapper<T> : IEfDbSetWrapper<T>
        where T : class, IDeletable
    {
        private readonly EfDbContext context;

        public EfDbSetWrapper(EfDbContext context)
        {
            this.context = context;
        }

        public IQueryable<T> AllWithoutDeleted
        {
            get
            {
                return this.context.Set<T>().Where(x => !x.IsDeleted);
            }
        }

        public IQueryable<T> AllIncludingDeleted
        {
            get
            {
                return this.context.Set<T>();
            }
        }

        public void Add(T entity)
        {
            DbEntityEntry entry = this.context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.context.Set<T>().Add(entity);
            }
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;

            var entry = this.context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.context.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
