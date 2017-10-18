namespace MVCHoldem.Data.UnitOfWork
{
    using MVCHoldem.Data.Contracts;

    public class EfUnitOfWork : IEfUnitOfWork
    {
        private readonly MsSqlDbContext context;

        public EfUnitOfWork(MsSqlDbContext context)
        {
            this.context = context;
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
