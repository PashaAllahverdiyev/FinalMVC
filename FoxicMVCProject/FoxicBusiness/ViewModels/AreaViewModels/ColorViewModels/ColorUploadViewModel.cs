using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Foxic.Business.ViewModels.AreaViewModels.ColorViewModels;

public class ColorUploadViewModel
{
    public int Id { get; set; }
    [Required]
    public string ColorName { get; set; } = null!;

    public IFormFile? Image { get; set; }

    public string? ColorImage { get; set; }
}
