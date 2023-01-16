using FakeItEasy;
using ROS;
using ROS.Controllers;
using ROS.Entity;
using ROS.Interface;
using Vostok.Logging.Console;
namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var repoC = A.Fake<IRepository<Cheque>>();
        var reposP = A.Fake<IRepository<Product>>();
        var reposS = A.Fake<IShopRepository>();
        var control = new PurchaseController(repoC, reposP, reposS, new ConsoleLog());
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
        }).Result;
        var products = new Product[]
        {
            new Product()
            {
                ProductName = "чипсы", ChequeId = chequeId, Id = Guid.NewGuid(), ProductPrice = 20,
            },
            new Product()
            {
                ProductName = "Хлеб", ChequeId = chequeId, Id = Guid.NewGuid(), ProductPrice = 40,
            }
        };
        Assert.Multiple(() =>
        {
            Assert.That("Магнит", Is.EqualTo(purchaseRequest.NameShop));
            Assert.That("Ленина 42", Is.EqualTo(purchaseRequest.Location));
            Assert.That(chequeId, Is.EqualTo(purchaseRequest.Products[0].ChequeId));
            for (var j = 0; j < purchaseRequest.Products.Length; j++)
                Assert.That(purchaseRequest.Products[j].ProductName, Is.EqualTo(products[j].ProductName));
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
            Date = DateTime.Now,
            Products = products
        });
    }
}