using Microsoft.AspNetCore.Http;

namespace Foxic.Business.ViewModels.AreaViewModels.ProductVM;

public class ProductCreateViewModel
{
    public string Name { get; set; }
    public double Price { get; set; }
    public IFormFile MainImage { get; set; }
    public List<IFormFile> Images { get; set; }
    public List<int> ColorIds { get; set; }
    public List<int> SizeIds { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public int CollectionId { get; set; }
    public int DetailId { get; set; }
}
