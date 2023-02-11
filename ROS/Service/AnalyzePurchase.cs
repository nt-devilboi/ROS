using ROS.Services;

namespace ROS.Service;

class AnalyzePurchase : IAnalyzePurchase
{
    public IPurchaseDetails PurchaseDetails { get; }
    public double GetAverageSum()
    {
        throw new NotImplementedException();
    }
    
    

    public double IsNewShop()
    {
        throw new NotImplementedException();
    }
} 