using Microsoft.EntityFrameworkCore;
using ROS.DataBase;
using ROS.Entity;

namespace ROS;

public class ProductRepository : IRepository<Product>
{
    private PurchaseContext _db = new PurchaseContext();

    public void Add(Product product)
    {
        _db.products.Add(new Product
        {
            Id = product.ChequeId,
            ChequeId = product.ChequeId,
            ProductName = product.ProductName,
            ProductPrice = product.ProductPrice
        });
    }
    
    public Task<List<Product>> ToList()
    {
        return Task.FromResult(_db.products.ToList());
    }

    public async Task<Product> Get(Guid guid)
    {
        return (await _db.products.FirstOrDefaultAsync(product => product.Id == guid))!;
    }

    public async Task<Product[]> GetAll(Guid chequeId)
    {
        return  Task.FromResult(_db.products.Where(product => product.ChequeId == chequeId)).Result.ToArray();
    }
    
    public async Task<Product[]> Where(Guid element)
    {
        return _db.products.Where(product => product.ChequeId == element).ToArray();
    }
    public void SaveChanges()
    {
        _db.SaveChanges();
    }

   

    public Task<Product> GetFirst(Guid element)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetFirst<TElement>(TElement element)
    {
        throw new NotImplementedException();
    }

}