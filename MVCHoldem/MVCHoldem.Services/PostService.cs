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
        private readonly IUserService userService;

        public PostService(IEfDbSetWrapper<Post> postsDbSet, IUserService userService)
        {
            Guard.WhenArgument(postsDbSet, "postsDbSet").IsNull().Throw();
            Guard.WhenArgument(userService, "userService").IsNull().Throw();

            this.postsDbSet = postsDbSet;
            this.userService = userService;
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

            return this.postsDbSet
                .AllWithoutDeleted
                .Where(p => p.Title.ToLower().Contains(searchTerm) ||
                            p.Description.ToLower().Contains(searchTerm) ||
                            p.Content.ToLower().Contains(searchTerm) ||
                            p.Author.UserName.ToLower().Contains(searchTerm))
                .AsEnumerable();
        }

        public IEnumerable<Post> AllIncludingDeleted()
        {
            return this.postsDbSet
                .AllIncludingDeleted
                .AsEnumerable();
        }

        public Post AddNewPost(string title, string description, string content, string author)
        {
            Guard.WhenArgument(author, "postsDbSet").IsNull().Throw();

            var user = this.userService
                .FindByUserName(author);

            Guard.WhenArgument(user, "user").IsNull().Throw();

            var post = new Post()
            {
                Title = title,
                Description = description,
                Content = content,
                Author = user
            };

            this.postsDbSet
                .Add(post);

            return post;
        }

        public void Update(Post post)
        {
            Guard.WhenArgument(post, "post").IsNull().Throw();

            this.postsDbSet
                .Update(post);
        }

        public void Delete(Post post)
        {
            Guard.WhenArgument(post, "post").IsNull().Throw();

            this.postsDbSet
                .Delete(post);
        }
        
        public void HardDelete(Post post)
        {
            Guard.WhenArgument(post, "post").IsNull().Throw();

            this.postsDbSet
                .HardDelete(post);
        }
    }
}
