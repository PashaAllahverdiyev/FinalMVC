using Microsoft.AspNetCore.Http;

namespace Foxic.Business.ViewModels.AreaViewModels.CollectionVM;

public class CollectionCreateViewModel
{
    public string CollectionName { get; set; }
    public IFormFile Image { get; set; }
}
