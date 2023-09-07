using Foxic.Core.Entities.AreaEntityController;
using Foxic.DataAccess.Contexts;
using Foxic.UI.ViewModels.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foxic.UI.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        HomeViewModel vm = new()
        {
            Sliders = await _context.Sliders.ToListAsync(),
            Categories = await _context.Categories.ToListAsync(),
            Collections = await _context.Collections.ToListAsync(),
            Products = await _context.Products.ToListAsync(),
            Brands = await _context.Brands.ToListAsync(),
        };
        return View(vm);
    }
}
