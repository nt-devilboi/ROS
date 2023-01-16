using System.Text.Json.Serialization;

namespace ROS.Entity;

public class Purchase
{
    public string NameShop { get; set; }
    public string Location { get; set; }
    public DateTime? Date { get; set; }
    public Product[] Products { get; set; } // shopId создается из навзание магазина и локаций
}