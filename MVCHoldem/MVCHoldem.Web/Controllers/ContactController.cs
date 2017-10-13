namespace MVCHoldem.Web.Controllers
{
    using System.Web.Mvc;
    using MVCHoldem.Web.ViewModels;

    public class ContactController : Controller
    {
        [HttpGet]
        public ActionResult Index(ContactViewModel model)
        {
            return this.View(model);
        }
    }
}