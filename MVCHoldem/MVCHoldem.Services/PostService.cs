namespace MVCHoldem.Services
{
    using System;
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
            return this.postsDbSet
                .AllWithoutDeleted
                .OrderByDescending(p => p.CreatedOn)
                .Take(MostRecentCount)
                .AsEnumerable();
        }

        public Post GetById(Guid id)
        {
            return this.postsDbSet
                .GetById(id);
        }

        public IEnumerable<Post> AllWithoutDeleted(string searchTerm = "")
        {
            searchTerm = searchTerm.ToLower();

            return this.postsDbSet.AllWithoutDeleted
                .Where(p => p.Title.ToLower().Contains(searchTerm) ||
                            p.Description.ToLower().Contains(searchTerm) ||
                            p.Content.ToLower().Contains(searchTerm) ||
                            p.Author.UserName.ToLower().Contains(searchTerm))
                .AsEnumerable();
        }
    }
}
