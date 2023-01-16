using Microsoft.EntityFrameworkCore;
using ROS.DataBase;
using ROS.Entity;

namespace ROS;

public class ProductRepository : IRepository<Product>
{
    private PurchaseContext _db = new PurchaseContext();

    public void Add(Product product)
    {
        _db.products.Add(product);
    }
    
    public Task<List<Product>> ToList()
    {
        return Task.FromResult(_db.products.ToList());
    }

    public async Task<Product> Get(Guid guid)
    {
        return (await _db.products.FirstOrDefaultAsync(product => product.Id == guid))!;
    }
    
    public async Task<Product[]> Where(Guid chequeId)
    {
        return  _db.products.Where(product => product.ChequeId == chequeId).ToArray();
    }
    public void SaveChanges()
    {
        _db.SaveChanges();
    }
    

}