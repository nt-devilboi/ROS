namespace ROS.Entity;

public interface IShop : IEntity<string>
{ 
    public string NameShop { get; set; } 
    public string Location { get; set; }
}