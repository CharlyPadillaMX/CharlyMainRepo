using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop2.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
