using ROS.Entity;

namespace ROS.Services;

public class PurchaseDetails : IPurchaseDetails
{
    public  Cheque Cheque { get; }
    public  Shop Shop { get; }
    public Product[] Products { get; }

    public PurchaseDetails(Purchase purchase)
    {
        Cheque = new Cheque()
        {
            ShopId = purchase.Location + purchase.NameShop,
            Time = purchase.Time, TotalAmount = purchase.Products.Sum(x => x.ProductPrice)
        };

        Shop = new Shop()
        {
            Id = purchase.Location + purchase.NameShop,
            Location = purchase.Location,
            NameShop = purchase.NameShop
        };
        
        
        Products = purchase.Products;
    }
}