namespace MVCHoldem.Data.SaveChanges
{
    using MVCHoldem.Data.Contracts;

    public class EfDbContextSaveChanges : IEfDbContextSaveChanges
    {
        private readonly EfDbContext context;

        public EfDbContextSaveChanges(EfDbContext context)
        {
            this.context = context;
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
