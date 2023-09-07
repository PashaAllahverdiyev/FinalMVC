using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Foxic.Business.ViewModels.AreaViewModels.BrandVM;

public class BrandUploadViewModel
{
    public int Id { get; set; }

    [Required, MaxLength(30), MinLength(5)]
    public string BrandName { get; set; } = null!;

    public IFormFile? Image { get; set; }

    public string? BrandImage { get; set; }
}
