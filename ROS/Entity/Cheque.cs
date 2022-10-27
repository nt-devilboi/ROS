namespace ROS.Entity;

public class Cheque : IChequeId
{
    public Guid ChequeId { get; set; }
    public string ShopId { get; set; }
    public double TotalAmount { get; set; }
    public DateTime Time { get; set; }
    
}