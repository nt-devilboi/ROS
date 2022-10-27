namespace ROS.Entity;

public interface IChequeId
{
    public Guid ChequeId { get; set; }
    public string ShopId { get; set; }
    public double TotalAmount { get; set; }
    public DateTime Time { get; set; }
}