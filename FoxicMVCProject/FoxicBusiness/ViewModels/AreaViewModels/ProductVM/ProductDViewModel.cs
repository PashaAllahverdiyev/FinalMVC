using Foxic.Core.Entities;

namespace Foxic.Business.ViewModels.AreaViewModels.ProductVM;

public class ProductDViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Discount { get; set; }
    public int Rating { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    public List<Image> Images { get; set; }
    public List<ProductColor> Colors { get; set; }
    public List<ProductSize> Sizes { get; set; }
}

