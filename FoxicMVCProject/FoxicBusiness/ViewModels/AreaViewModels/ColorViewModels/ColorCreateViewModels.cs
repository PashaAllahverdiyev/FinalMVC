using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Foxic.Business.ViewModels.AreaViewModels.ColorViewModels;

public class ColorCreateViewModels
{
    [Required]
    public string ColorName { get; set; } = null!;
    [Required]
    public IFormFile ColorUrl { get; set; }
}
