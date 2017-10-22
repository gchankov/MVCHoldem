namespace MVCHoldem.Web.Controllers
{
    using System.Web.Mvc;

    public class ChatController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return this.View();
        }
    }
}