namespace Foxic.Core.Entities;

public class ProductDetails : BaseEntity
{
    public string ShortDesc {get; set;}
    public string LongDesc { get; set;}
    public bool Cotton { get; set;}
    public bool Polyester { get; set;}
    public bool Clean { get; set;}
    public bool NonChlorinne { get; set;}
    public bool Tax { get; set;}
}
