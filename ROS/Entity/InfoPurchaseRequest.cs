namespace ROS.Entity;

public class InfoPurchaseRequest
{
    public string NameShop { get; set; }
    public string Location { get; set; }
    public DateTime Time { get; set; }
    public List<Product> Products { get; set; } // shopId создается из навзание магазина и локаций
}