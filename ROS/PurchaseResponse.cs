using ROS.Entity;

namespace ROS;

public class PurchaseResponse
{
    public Shop Shop { get; set; }
    public Cheque Cheque { get; set; }
    public List<Product> Products { get; set; }
}