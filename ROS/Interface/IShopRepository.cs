using ROS.Entity;

namespace ROS.Interface;

public interface IShopRepository
{
    public void Add(Shop entity);
    public Task<List<Shop>> ToList();
    
    public Task<Shop[]> Where(string element);
    public void SaveChanges();
    public Task<Shop> Get(string id);
}