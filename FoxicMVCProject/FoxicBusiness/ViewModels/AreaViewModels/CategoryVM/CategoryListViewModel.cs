using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Foxic.Business.ViewModels.CategoryVM;

public class CategoryListViewModel
{
    [Required, MaxLength(50), MinLength(10)]
    public string CategoryName { get; set; }
    public IFormFile Image { get; set; }
}
