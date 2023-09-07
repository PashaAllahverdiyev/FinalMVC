using Microsoft.AspNetCore.Http;

namespace Foxic.Business.ViewModels.AreaViewModels.BrandVM;

public class BrandCreateViewModel
{
    public string BrandName { get; set; }
    public IFormFile Image { get; set; }
    public string BrandUrl { get; set; }
}
