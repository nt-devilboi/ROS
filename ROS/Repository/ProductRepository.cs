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
            ProductId = product.ChequeId,
            ChequeId = product.ChequeId,
            ProductName = product.ProductName,
            ProductPrice = product.ProductPrice
        });
    }

    public Task<List<Product>> ToList()
    {
        return Task.FromResult(_db.products.ToList());
    }

    public Task<Product> Get(Guid guid)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}