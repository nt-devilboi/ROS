using ROS.Services;

namespace ROS.Service;

public interface IAnalyzePurchase
{
    public IPurchaseDetails PurchaseDetails { get;}
    

    public double GetAverageSum();
    public double IsNewShop();
    
}