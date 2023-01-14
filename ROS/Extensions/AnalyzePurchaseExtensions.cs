using ROS.Entity;
using ROS.Services;

namespace ROS.TemporaryBd;

public static class AnalyzePurchaseExtensions
{
    public static double AverageSum(this Purchase purchase)
    => purchase.Products.Sum(x => x.ProductPrice) / purchase.Products.Length;
    
    public static Purchase ToPurchase(this PurchaseDetails purchaseDetails) 
    {
        return new Purchase()
        {
            NameShop = purchaseDetails.Shop.NameShop,
            Location = purchaseDetails.Shop.Location,
            Products = purchaseDetails.Products,
            Time = purchaseDetails.Cheque.Time
        };
    }

    public static PurchaseDetails ToPurchaseDetails(this Purchase purchase)
    {
        return new PurchaseDetails(purchase);
    }

    public static Purchase Join(this Cheque cheque, Product[] product, Shop shop)
    {
        return new Purchase()
        {
            Location = shop.Location,
            NameShop = shop.NameShop,
            Products = product,
            Time = cheque.Time
        };
    }
}