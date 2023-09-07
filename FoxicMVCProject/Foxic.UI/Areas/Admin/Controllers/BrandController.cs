using AutoMapper;
using Foxic.Business.Exceptions;
using Foxic.Business.Services.Implemetations;
using Foxic.Business.Services.Interfaces;
using Foxic.Business.ViewModels.AreaViewModels.BrandVM;
using Foxic.Core.Entities;
using Foxic.DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foxic.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webEnv;
        private readonly IFileService _fileService;
        public BrandController(AppDbContext context,
                               IWebHostEnvironment webEnv,
                               IFileService fileService)
        {
            _context = context;
            _webEnv = webEnv;
            _fileService = fileService;
        }
        public IActionResult Index(int Id)
        {
            Brand brand = _context.Brands.FirstOrDefault(x => x.Id == Id);
            List<Brand> brands = _context.Brands.ToList();
            return View(brands);
        }
        public async Task<IActionResult> Details(int id)
        {
            Brand? brand = _context.Brands.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (brand == null) return NotFound();
            return View(brand);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandCreateViewModel brand)
        {
            string filename = string.Empty;

            Brand brand1 = new()
            {
                BrandName = brand.BrandName
            };
            filename = await _fileService.UploadFile(brand.Image, _webEnv.WebRootPath, 300, "assets", "images", "slider");
            brand1.Image = filename;
            await _context.Brands.AddAsync(brand1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Brand? brand = await _context.Brands.FindAsync(id);
            if (brand == null) return NotFound();
            return View(brand);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            Brand? brand = await _context.Brands.FindAsync(id);
            if (brand == null) return NotFound();
            string fileroot = Path.Combine(_webEnv.WebRootPath, brand.Image);
            if (System.IO.File.Exists(fileroot))
            {
                System.IO.File.Delete(fileroot);
            }
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int id)
        {
            Brand? brand = await _context.Brands.FindAsync(id);
            if (brand == null) return NotFound();
            BrandUploadViewModel brandUpload = new()
            {
                Id = brand.Id,
                BrandName = brand.BrandName,
                BrandImage = brand.Image,
            };
            return View(brandUpload);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, BrandUploadViewModel brand)
        {
            if (!ModelState.IsValid) return View(brand);
            Brand? branddb = await _context.Brands.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
            if (branddb == null) return NotFound();
            if (brand.Image != null)
            {
                try
                {
                    string filename = await _fileService.UploadFile(brand.Image, _webEnv.WebRootPath, 300, "assets", "images", "slider");
                    _fileService.RemoveFile(_webEnv.WebRootPath, branddb.Image);
                    Brand brandd = new()
                    {
                        Id = brand.Id,
                        BrandName = brand.BrandName,
                        Image = brand.BrandImage
                    };
                    branddb = brandd;
                    branddb.Image = filename;
                }
                catch (FileSizeException ex)
                {
                    ModelState.AddModelError("Image", ex.Message);
                    return View(brand);
                }
                catch (FileTypeException ex)
                {
                    ModelState.AddModelError("Image", ex.Message);
                    return View(brand);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(brand);
                }
            }
            else
            {
                brand.BrandImage = branddb.Image;
                Brand brandd = new()
                {
                    Id = brand.Id,
                    BrandName = brand.BrandName,
                    Image = brand.BrandImage
                };
                branddb = brandd;
            }
            _context.Brands.Update(branddb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
    }
