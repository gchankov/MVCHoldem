namespace MVCHoldem.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using Bytes2you.Validation;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.Attributes;
    using MVCHoldem.Web.Extensions;
    using MVCHoldem.Web.ViewModels.Post;

    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService postsService)
        {
            Guard.WhenArgument(postsService, "postsService").IsNull().Throw();

            this.postService = postsService;
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var post = this.postService.GetById(id);

            Guard.WhenArgument(post, "post").IsNull().Throw();

            var postDetailsViewModel = MappingService.Provider.Map<PostDetailsViewModel>(post);

            return this.View(postDetailsViewModel);
        }

        [HttpGet]
        public ActionResult All()
        {
            return this.View();
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult AllPostsWithoutDeleted()
        {
            var allPostsViewModel = this.postService
                .AllWithoutDeleted()
                .Map<Post, AllPostsViewModel>();

            return this.PartialView("_AllPostsPartial", allPostsViewModel);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult FilteredPosts(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return this.AllPostsWithoutDeleted();
            }
            else
            {
                var filteredPostsViewModel = this.postService
                    .AllWithoutDeleted(searchTerm)
                    .Map<Post, AllPostsViewModel>();

                return this.PartialView("_AllPostsPartial", filteredPostsViewModel);
            }
        }
    }
}