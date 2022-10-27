using ROS.Entity;

namespace ROS.Interface;

public interface IInfoAboutPurchase
{
        public Shop shop { get; }
        public Cheque Cheque { get; }
        public Product[] Products { get; } // shopId создается из навзание магазина и локаций
}