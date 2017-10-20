namespace MVCHoldem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Bytes2you.Validation;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.Extensions;
    using MVCHoldem.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IPostService postsService;

        public HomeController(IPostService postsService)
        {
            Guard.WhenArgument(postsService, "postsService").IsNull().Throw();

            this.postsService = postsService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var mostRecentPosts = this.postsService
                .GetMostRecent()
                .Map<Post, MostRecentPostViewModel>()
                .ToList();

            Guard.WhenArgument(mostRecentPosts, "mostRecentPosts").IsNull().Throw();

            var homeViewModel = new HomeViewModel()
            {
                MostRecentPosts = mostRecentPosts
            };

            return this.View(homeViewModel);
        }
    }
}