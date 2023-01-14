using ROS.Entity;

namespace ROS.Services;

public interface IPurchaseDetails
{
    public  Cheque Cheque { get; }
    public  Shop Shop { get; }
    public  Product[] Products { get; }
    
}