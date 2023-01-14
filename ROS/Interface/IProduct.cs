namespace ROS.Entity;

public interface IProduct : IEntity<Guid>
{
    public double ProductPrice { get; set; }
    public string ProductName { get; set; }
    public Guid ChequeId { get; set; }
}