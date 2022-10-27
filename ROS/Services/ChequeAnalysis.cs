using ROS.Entity;

namespace ROS.Services;

public class Purchase : IPurchase
{
    public  Cheque Cheque { get; }
    public  Shop Shop { get; }
    public List<Product> Products { get; }

    public Purchase(InfoPurchaseRequest infoPurchaseRequest)
    {
        Cheque = new Cheque()
        {
            ShopId = infoPurchaseRequest.Location + infoPurchaseRequest.NameShop,
            Time = infoPurchaseRequest.Time, TotalAmount = infoPurchaseRequest.Products.Sum(x => x.ProductPrice)
        };

        Shop = new Shop()
        {
            ShopId = infoPurchaseRequest.Location + infoPurchaseRequest.NameShop,
            Location = infoPurchaseRequest.Location,
            NameShop = infoPurchaseRequest.NameShop
        };
        
        foreach (var product in infoPurchaseRequest.Products)
        {
            product.ChequeId = Cheque.ChequeId;
        }

        Products = infoPurchaseRequest.Products;
    }
}