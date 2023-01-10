using ROS.Entity;

namespace ROS.Services;

public interface IPurchase
{
    public  Cheque Cheque { get; }
    public  Shop Shop { get; }
    public  Product[] Products { get; }
    
}