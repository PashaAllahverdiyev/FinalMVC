using Microsoft.AspNetCore.Mvc;

namespace Foxic.UI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
		public IActionResult Detail()
		{
			return View();
		}
	}
}
