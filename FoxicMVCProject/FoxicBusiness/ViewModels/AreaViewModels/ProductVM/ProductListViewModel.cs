using Foxic.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;

namespace Foxic.Business.ViewModels.AreaViewModels.ProductVM;

public class ProductListViewModel
{
    public int Id { get; set; }

    public string Name { get; set; }
    public int Discount { get; set; }
    public int Stock { get; set; }
    public IFormFile MainImage { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }
    public Collection Collection { get; set; }
    public Brand Brand { get; set; }
    public string Images { get; set; }

}
