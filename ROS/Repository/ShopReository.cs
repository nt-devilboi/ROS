using Microsoft.EntityFrameworkCore;
using ROS.DataBase;
using ROS.Entity;
using ROS.Interface;

namespace ROS;

public class ShopRepository : IShopRepository
{
    private PurchaseContext _db = new PurchaseContext();

    public void Add(Shop entity)
    {
        _db.shops.Add(entity);
    }
    
    public async Task<Shop> Get(string id)
    {
        return (await _db.shops.FirstOrDefaultAsync(shop => shop.Id == id))!;
    }

    public async Task<List<Shop>> ToList()
    {
        return await _db.shops.ToListAsync();
    }

    public Task<Shop[]> Where(string element)
    {
        throw new NotImplementedException();
    }
    
    public void SaveChanges()
    {
        _db.SaveChanges();
    }

   
}