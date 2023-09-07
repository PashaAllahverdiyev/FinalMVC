using AutoMapper;
using Foxic.Business.Services.Interfaces;
using Foxic.Business.Utilities;
using Foxic.Business.ViewModels.AreaViewModels.ProductVM;
using Foxic.Core.Entities;
using Foxic.DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace Foxic.UI.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webEnv;
    private readonly IFileService _fileservice;
    public ProductController(AppDbContext context,
                            IWebHostEnvironment webEnv,
                            IFileService fileservice)
    {
        _context = context;
        _webEnv = webEnv;
        _fileservice = fileservice;
    }
    public IActionResult Index()
    {
        List<ProductListViewModel> product = _context.Products.Select(p => new ProductListViewModel()
        {
            Name = p.Name,
            Images = p.Images.FirstOrDefault(i => i.IsMain.Equals(true)).Url,
        }).ToList();


        return View(product);
    }
    public IActionResult Create()
    {
        ViewBag.Colors = _context.Colors.ToList();
        ViewBag.Sizes = _context.Sizes.ToList();
        ViewBag.Brands = _context.Brands.ToList();
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>Create(ProductCreateViewModel model)
    {
        ViewBag.Colors = _context.Colors.ToList();
        ViewBag.Sizes = _context.Sizes.ToList();
        ViewBag.Brands = _context.Brands.ToList();

        string filename = string.Empty;
        Product newProduct = new()
        {
            Name = model.Name,
            Price = model.Price,
            CollectionId =model.CollectionId,
            CategoryId = model.CategoryId,
            DetailId = model.DetailId,
            BrandId = model.BrandId,
        };
        filename = await _fileservice.UploadFile(model.MainImage, _webEnv.WebRootPath, 300, "assets", "images", "slider");
        Image MainImage = new()
        {
            IsMain = true,
            Url = filename
        };
        newProduct.Images.Add(MainImage);
        foreach(IFormFile image in model.Images)
        {
            if (!image.CheckFileSize(1000))
            {
                return View(nameof(Create));
            };
            if (!image.CheckFileType("image/"))
            {
                return View(nameof(Create));
            }
            Image NotMainImage = new()
            {
                IsMain = false,
                Url = filename
            };
            newProduct.Images.Add(NotMainImage);

        }
        foreach(int id in model.ColorIds)
        {
            ProductColor productColor = new()
            {
                ColorId = id,
            };
            newProduct.Colors.Add(productColor);
        }
        foreach(int id in model.SizeIds)
        {
            ProductSize productSize = new()
            {
                SizeId = id,
            };
            newProduct.Sizes.Add(productSize);
        }
        _context.Products.Add(newProduct);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}