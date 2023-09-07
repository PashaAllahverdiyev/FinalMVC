using Foxic.Business.Exceptions;
using Foxic.Business.Services.Interfaces;
using Foxic.Business.ViewModels.AreaViewModels.ColorViewModels;
using Foxic.Core.Entities;
using Foxic.DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Foxic.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
	public class ColorController : Controller
	{
		private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _webEnv;
		private readonly IFileService _fileService;
		public ColorController(AppDbContext context,
							   IWebHostEnvironment webEnv,
							   IFileService fileService)
		{
			_context = context;
			_webEnv = webEnv;
			_fileService = fileService;
		}
		public IActionResult Index(int Id)
		{
			Color color = _context.Colors.FirstOrDefault(x=>x.Id==Id);
			List<Color>colors=_context.Colors.ToList();
			return View(colors);
		}
		public async Task<IActionResult>Details(int id)
		{
			Color? color = _context.Colors.AsNoTracking().FirstOrDefault(c => c.Id == id);
			if(color==null)return NotFound();
			return View(color);
		}
		public IActionResult Create()
		{
			return View();
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ColorCreateViewModels color)
        {
            string filename = string.Empty;

            Color color1 = new()
            {
                Name = color.ColorName
            };
            filename = await _fileService.UploadFile(color.ColorUrl, _webEnv.WebRootPath, 300, "assets", "images", "slider");
            color1.Image = filename;
            await _context.Colors.AddAsync(color1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Color? color = await _context.Colors.FindAsync(id);
            if (color == null) return NotFound();
            return View(color);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            Color? color = await _context.Colors.FindAsync(id);
            if (color == null) return NotFound();
            string fileroot = Path.Combine(_webEnv.WebRootPath, color.Image);
            if (System.IO.File.Exists(fileroot))
            {
                System.IO.File.Delete(fileroot);
            }
            _context.Colors.Remove(color);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int id)
        {
            Color? color = await _context.Colors.FindAsync(id);
            if (color == null) return NotFound();
            ColorUploadViewModel colorUpload = new()
            {
                Id = color.Id,
                ColorName = color.Name,
                ColorImage = color.Image,
            };
            return View(colorUpload);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, ColorUploadViewModel color)
        {
            if (!ModelState.IsValid) return View(color);
            Color? colordb = await _context.Colors.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (colordb == null) return NotFound();
            if (color.Image != null)
            {
                try
                {
                    string filename = await _fileService.UploadFile(color.Image, _webEnv.WebRootPath, 300, "assets", "images", "slider");
                    _fileService.RemoveFile(_webEnv.WebRootPath, colordb.Image);
                    Color colorr = new()
                    {
                        Id = color.Id,
                        Name = color.ColorName,
                        Image = color.ColorImage
                    };
                    colordb = colorr;
                    colordb.Image = filename;
                }
                catch (FileSizeException ex)
                {
                    ModelState.AddModelError("Image", ex.Message);
                    return View(color);
                }
                catch (FileTypeException ex)
                {
                    ModelState.AddModelError("Image", ex.Message);
                    return View(color);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(color);
                }
            }
            else
            {
                color.ColorImage = colordb.Image;
                Color colorr = new()
                {
                    Id = color.Id,
                    Name = color.ColorName,
                    Image = color.ColorImage
                };
                colordb = colorr;
            }
            _context.Colors.Update(colordb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
