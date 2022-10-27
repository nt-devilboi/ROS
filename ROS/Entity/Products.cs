namespace ROS.Entity;

public class Product : IProduct
{
    public Guid ProductId { get; set; }
    public double ProductPrice { get; set; }
    public string ProductName { get; set; }
    public Guid ChequeId { get; set; }
}