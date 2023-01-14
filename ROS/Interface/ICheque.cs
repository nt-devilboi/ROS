namespace ROS.Entity;

public interface ICheque : IEntity<Guid>
{
    public string ShopId { get; set; }
    public double TotalAmount { get; set; }
    public DateTime? Time { get; set; }
}