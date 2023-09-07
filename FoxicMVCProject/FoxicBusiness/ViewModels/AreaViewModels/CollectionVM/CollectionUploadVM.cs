using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Foxic.Business.ViewModels.AreaViewModels.CollectionVM;

public class CollectionUploadVM
{
    public int Id { get; set; }

    [Required, MaxLength(30), MinLength(5)]
    public string CollectionName { get; set; } = null!;

    public IFormFile? Image { get; set; }

    public string? CollectionImage { get; set; }    
}
