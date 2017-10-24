namespace MVCHoldem.Web.Areas.Admin.Controllers
{
    using System;
    using System.Web.Mvc;
    using Bytes2you.Validation;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.Areas.Admin.ViewModels;
    using MVCHoldem.Web.Extensions;
    using MVCHoldem.Web.Infrastructure.Attributes;

    [Authorize(Roles = "Admin")]
    public class PostAdminController : Controller
    {
        private readonly IPostService postService;

        public PostAdminController(IPostService postService)
        {
            Guard.WhenArgument(postService, "postService").IsNull().Throw();
            this.postService = postService;
        }
        
        public ActionResult Index()
        {
            return this.View();
        }
        
        public ActionResult ReadPosts([DataSourceRequest] DataSourceRequest request)
        {
            Guard.WhenArgument(request, "request").IsNull().Throw();

            var postsGridViewModel = this.postService
                .AllIncludingDeleted()
                .Map<Post, PostGridViewModel>()
                .ToDataSourceResult(request);

            return this.Json(postsGridViewModel);
        }

        [SaveChanges]
        public ActionResult CreatePost(PostGridViewModel postGridViewModel)
        {
            if (postGridViewModel != null)
            {
                var post = this.postService
                    .AddNewPost(
                        postGridViewModel.Title,
                        postGridViewModel.Description,
                        postGridViewModel.Content,
                        User.Identity.Name);

                postGridViewModel = MappingService.Provider.Map<PostGridViewModel>(post);
            }

            return this.Json(new[] { postGridViewModel });
        }

        [SaveChanges]
        public ActionResult UpdatePost(PostGridViewModel postGridViewModel)
        {
            if (postGridViewModel != null)
            {
                var post = this.postService
                    .GetById(new Guid(postGridViewModel.Id));

                post.Title = postGridViewModel.Title;
                post.Description = postGridViewModel.Description;
                post.Content = postGridViewModel.Content;

                if (post.IsDeleted && !postGridViewModel.IsDeleted)
                {
                    post.IsDeleted = postGridViewModel.IsDeleted;
                    post.DeletedOn = null;
                }

                this.postService.Update(post);

                if (!post.IsDeleted && postGridViewModel.IsDeleted)
                {
                    this.postService.Delete(post);
                    postGridViewModel.DeletedOn = post.DeletedOn;
                }
            }

            return this.Json(new[] { postGridViewModel });
        }
        
        [SaveChanges]
        public ActionResult HardDeletePost(PostGridViewModel postGridViewModel)
        {
            if (postGridViewModel != null)
            {
                var post = this.postService.GetById(new Guid(postGridViewModel.Id));
                this.postService.HardDelete(post);
            }

            this.HttpContext.Cache.Remove("mostRecentPosts");

            return this.Json(new[] { postGridViewModel });
        }
    }
}