using Foxic.Business.ViewModels.AreaViewModels.ProductDetailViewModels;
using Foxic.Core.Entities;
using Foxic.DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foxic.UI.Areas.Admin.Controllers;
[Area("Admin")]

public class ProductDetailController : Controller
{

    private readonly AppDbContext _context;

    public ProductDetailController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Index(int id)
    {
        ProductDetails productDetail = _context.ProductDetails.FirstOrDefault(p => p.Id == id);
        List<ProductDetails> productDetails = _context.ProductDetails.ToList();
        return View(productDetails);
    }
    public async Task<IActionResult> Details(int id)
    {
        ProductDetails? productDetail = await _context.ProductDetails.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (productDetail == null) return NotFound();
        return View(productDetail);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductDetailCreateViewModel product)
    {
        if (!ModelState.IsValid) return View(product);
        ProductDetails productDetails = new()
        {
            LongDesc = product.LongDescription,
            ShortDesc = product.ShortDescription,
            Clean = product.Clean,
            Cotton = product.Cotton,
            NonChlorinne = product.Non_Chlorine,
            Polyester = product.Polyester,
            Tax = product.Tax,
        };
        await _context.ProductDetails.AddAsync(productDetails);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        ProductDetails? productDetail = await _context.ProductDetails.FindAsync(id);
        if (productDetail == null) return NotFound();
        return View(productDetail);
    }
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>DeletePost(int id)
    {
        ProductDetails? productDetails = await _context.ProductDetails.FindAsync(id);
        if (productDetails == null) return NotFound();
        _context.ProductDetails.Remove(productDetails);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
