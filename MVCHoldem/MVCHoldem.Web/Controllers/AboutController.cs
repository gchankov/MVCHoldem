namespace MVCHoldem.Web.Controllers
{
    using System.Web.Mvc;
    using MVCHoldem.Web.ViewModels;

    public class AboutController : Controller
    {
        [HttpGet]
        public ActionResult Index(AboutViewModel model)
        {
            return this.View(model);
        }
    }
}