using Microsoft.AspNetCore.Mvc;
using ROS.Entity;
using ROS.Interface;
using ROS.Services;
using ROS.TemporaryBd;
using Vostok.Logging.Abstractions;

namespace ROS.Controllers;

[ApiController]
[Route("[controller]")]
public class PurchaseController : ControllerBase
{
    private IRepository<Cheque> _chequeRepository;
    private IRepository<Product> _productRepository;
    private IShopRepository _shopRepository;
    private ILog _log;

    public PurchaseController(IRepository<Cheque> chequeRepository,
        IRepository<Product> productRepository,
        IShopRepository shopRepository,
        ILog log)
    {
        _shopRepository = shopRepository;
        _chequeRepository = chequeRepository;
        _productRepository = productRepository;
        _log = log;
    }

    [HttpGet]
    [Route("/getcheques")]
    public async Task<Purchase[]> Get()
    {
        var purchases = new List<Purchase>();
        foreach (var cheque in  await _chequeRepository.ToList())
            purchases.Add(await GetPurchase(cheque.Id));

        return purchases.ToArray();
    }

    [HttpGet]
    [Route("/cheques/cheque")]
    public async Task<Purchase> GetPurchase(Guid chequeId)
    {
        var cheque = await _chequeRepository.Get(chequeId);
        var product = await _productRepository.Where(chequeId);
        var shop = await _shopRepository.Get(cheque.ShopId);
        var purchase = cheque.Join(product, shop);
        return purchase;
    }

    [HttpPost]
    [Route("/addpurchase")]
    public async Task<Purchase> AddPurchase([FromBody] Purchase purchase)
    {
        var purchaseInfo = new PurchaseDetails(purchase);
        _chequeRepository.Add(purchaseInfo.Cheque);
        _chequeRepository.SaveChanges();

        _shopRepository.Add(purchaseInfo.Shop);
        _shopRepository.SaveChanges();

        foreach (var product in purchase.Products) // написать отельный  для работы с репозиториями
        {
            _productRepository.Add(product);
            _productRepository.SaveChanges();
        }

        _log.Info("complete");
        return purchaseInfo.ToPurchase();
    }
}