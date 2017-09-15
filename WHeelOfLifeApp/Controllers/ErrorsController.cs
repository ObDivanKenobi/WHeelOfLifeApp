using System.Web.Mvc;

namespace WHeelOfLifeApp.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult UnauthorizedRequest()
        {
            return View();
        }
    }
}