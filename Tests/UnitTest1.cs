using ROS;
using ROS.Controllers;
using ROS.Entity;
using ROS.Interface;
using Vostok.Logging.Console;

namespace Tests;

public class Tests
{
    class FakeRepository<T> : IRepository<T>
    {
        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ToList()
        {
            throw new NotImplementedException();
        }

        public Task<T[]> Where(Guid element)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetFirst(Guid element)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }

    
    class FakeShopRepository : IShopRepository
    {
        public void Add(Shop entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Shop>> ToList()
        {
            throw new NotImplementedException();
        }

        public Task<Shop[]> Where(string element)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<Shop> Get(string id)
        {
            throw new NotImplementedException();
        }
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var control = new PurchaseController(new FakeRepository<Cheque>(),new FakeRepository<Product>(), new FakeShopRepository(), new ConsoleLog());
        var chequeId = Guid.NewGuid();
        var purchaseRequest = CreatePurchase(control, "Ленина 42", "Магнит", new Product[]
        {
            new Product()
            {
                ProductName = "чипсы", ChequeId = chequeId, Id = Guid.NewGuid(), ProductPrice = 20,
            },
            new Product()
            {
                ProductName = "Хлеб", ChequeId = chequeId, Id = Guid.NewGuid(), ProductPrice = 40,
            }
        });

        var purchaseResponse = control.GetPurchase(chequeId);
        Assert.Multiple(() =>
        {
           Assert.Equals()
        });
    }

    private static Task<Purchase> CreatePurchase(PurchaseController control,
        string location,
        string name,
        Product[] products)
    {
        return control.AddPurchase(new Purchase()
        {
            Location = location,
            NameShop = name,
            Time = DateTime.Now,
            Products = products
        });
    }
}