using Microsoft.AspNetCore.Mvc;

namespace CoreBookStore.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {                
            return View();
        }
    }
}