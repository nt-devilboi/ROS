namespace ROS.Entity;

public class PurchaseInfo
{
    public Guid Id { get; set; }
    public string NameShop { get; set; }
    public double TotalAmount { get; set; }
    public string Location { get; set; }
}