using Microsoft.AspNetCore.Mvc;

namespace Foxic.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
