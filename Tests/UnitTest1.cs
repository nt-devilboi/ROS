using System.Data.Common;
using System.Security.Cryptography;
using ROS;
using ROS.Controllers;
using ROS.Entity;
using Vostok.Logging.Console;

namespace Tests;

public class Tests
{
    class FakeChequeRepository : IChequeRepository
    {
        public void Create(string shopId, double totalAmount, DateTime time)
        {
        }

        public Task<List<Cheque>> ToList()
        {
            throw new NotImplementedException();
        }

        public Task<Cheque> Get(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
        }
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var control = new ChequeController(new FakeChequeRepository(), new ConsoleLog());
        var products = new List<Product> { new () { ProductName = "apple", ProductPrice = 30 } };
        var infoPurchase = CreatePurchase(control, "Комсомльская 78", "кб", products);
        
        Assert.Multiple(() =>
        {
            Assert.That(infoPurchase.Result.Value!.Shop.Location, Is.EqualTo("Комсомльская 78"));
            Assert.That(infoPurchase.Result.Value!.Shop.NameShop, Is.EqualTo("кб"));
            Assert.That(infoPurchase.Result.Value.Products[0].ProductPrice, Is.EqualTo(30));
            Assert.That(infoPurchase.Result.Value.Shop.ShopId, Is.EqualTo("Комсомльская 78кб"));
        });
    }

    private static Task<Result<PurchaseResponse>> CreatePurchase(ChequeController control,
        string location,
        string name,
        List<Product> products)
    {
        return control.AddPurchase(new InfoPurchaseRequest()
        {
            Location = location,
            NameShop = name,
            Time = DateTime.Now,
            Products = products
        });
    }
}