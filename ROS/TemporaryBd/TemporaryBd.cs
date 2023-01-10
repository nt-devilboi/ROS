using ROS.Entity;

namespace ROS.TemporaryBd;

public static class TemporaryBd
{
    public static List<Cheque> GetSimpleCheque()
    {
        return new List<Cheque>()
        {
            new Cheque{
                ChequeId = Guid.NewGuid(), ShopId = "kbnear", TotalAmount = 300, Time = null
            },
            new Cheque{
                ChequeId = Guid.NewGuid(), ShopId = "Fiveland", TotalAmount = 9000, Time = null
            },
            new Cheque{
                ChequeId = Guid.NewGuid(), ShopId = "Magnit", TotalAmount = 4000, Time = null
            }
        };
    }
}