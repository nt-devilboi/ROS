using ROS.Entity;
using ROS.Services;

namespace ROS.TemporaryBd;

public static class AnalyzePurchaseExtensions
{
    public static double AverageSum(this Purchase purchase)
    => purchase.Products.Sum(x => x.ProductPrice) / purchase.Products.Length; // никита это мусор я фиг знает, зачем я это написал
    
    public static Purchase ToPurchase(this PurchaseDetails purchaseDetails) 
    {
        return new Purchase()
        {
            NameShop = purchaseDetails.Shop.NameShop,
            Location = purchaseDetails.Shop.Location,
            Products = purchaseDetails.Products,
            Date = purchaseDetails.Cheque.Time
        };
    }
    

    public static Purchase Join(this Cheque cheque, Product[] product, Shop shop) // кста, нету защиты данных можешь один чек с разными данными соеденять круто чтож)) 
    {
        return new Purchase()
        {
            Location = shop.Location,
            NameShop = shop.NameShop,
            Products = product,
            Date = cheque.Time
        };
    }
}