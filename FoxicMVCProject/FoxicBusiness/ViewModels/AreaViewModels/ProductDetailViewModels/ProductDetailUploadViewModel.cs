using System.ComponentModel.DataAnnotations;

namespace Foxic.Business.ViewModels.AreaViewModels.ProductDetailViewModels
{
    public class ProductDetailUploadViewModel
    {
        [Required, MaxLength(150), MinLength(5)]
        public string ShortDescription { get; set; }

        [Required, MaxLength(200), MinLength(5)]
        public string LongDescription { get; set; }
        public int Id { get; set; }

        public bool Cotton { get; set; }
        public bool Polyester { get; set; }
        public bool Clean { get; set; }
        public bool Non_Chlorine { get; set; }
        public bool Tax { get; set; }
    }
}
