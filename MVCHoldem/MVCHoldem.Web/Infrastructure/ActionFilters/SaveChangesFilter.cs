namespace MVCHoldem.Web.Infrastructure.ActionFilters
{
    using System.Web.Mvc;
    using Bytes2you.Validation;
    using MVCHoldem.Data.Contracts;

    public class SaveChangesFilter : IActionFilter
    {
        private readonly IEfDbContextSaveChanges contextSaveChanges;

        public SaveChangesFilter(IEfDbContextSaveChanges contextSaveChanges)
        {
            Guard.WhenArgument(contextSaveChanges, "contextSaveChanges").IsNull().Throw();
            this.contextSaveChanges = contextSaveChanges;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.contextSaveChanges.SaveChanges();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Just to satisfy the interface. Cannot decouple from it.
        }
    }
}