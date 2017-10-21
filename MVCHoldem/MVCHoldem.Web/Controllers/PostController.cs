namespace MVCHoldem.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using Bytes2you.Validation;
    using MVCHoldem.Services;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.ViewModels;

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
    }
}