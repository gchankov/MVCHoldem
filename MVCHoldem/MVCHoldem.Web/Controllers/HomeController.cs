namespace MVCHoldem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Bytes2you.Validation;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.Extensions;
    using MVCHoldem.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IPostService postService;

        public HomeController(IPostService postService)
        {
            Guard.WhenArgument(postService, "postsService").IsNull().Throw();

            this.postService = postService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (this.HttpContext.Cache["mostRecentPosts"] == null)
            {
                var mostRecentPosts = this.postService
                    .GetMostRecent()
                    .Map<Post, MostRecentPostViewModel>()
                    .ToList();

                Guard.WhenArgument(mostRecentPosts, "mostRecentPosts").IsNull().Throw();

                this.HttpContext.Cache.Insert(
                    "mostRecentPosts",
                    mostRecentPosts,
                    null,
                    DateTime.Now.AddMinutes(30D),
                    TimeSpan.Zero);
            }

            var homeViewModel = new HomeViewModel()
            {
                MostRecentPosts = (List<MostRecentPostViewModel>)this.HttpContext.Cache["mostRecentPosts"]
            };

            return this.View(homeViewModel);
        }
    }
}