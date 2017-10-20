namespace MVCHoldem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Bytes2you.Validation;
    using MVCHoldem.Data.Contracts;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services.Contracts;

    public class PostService : IPostService
    {
        private const int MostRecentCount = 5;

        private readonly IEfDbSetWrapper<Post> postsDbSet;
        private readonly IEfDbContextSaveChanges contextSaveChanges;

        public PostService(IEfDbSetWrapper<Post> postsDbSet, IEfDbContextSaveChanges contextSaveChanges)
        {
            Guard.WhenArgument(postsDbSet, "postsDbSet").IsNull().Throw();
            Guard.WhenArgument(contextSaveChanges, "contextSaveChanges").IsNull().Throw();

            this.postsDbSet = postsDbSet;
            this.contextSaveChanges = contextSaveChanges;
        }

        public IEnumerable<Post> GetMostRecent()
        {
            return this.postsDbSet.AllWithoutDeleted
                .OrderByDescending(p => p.CreatedOn)
                .Take(MostRecentCount)
                .AsEnumerable();
        }
    }
}
