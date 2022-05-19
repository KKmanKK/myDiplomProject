using Microsoft.AspNetCore.Mvc;

namespace DiplomStore.Controllers
{
    public class TitleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
