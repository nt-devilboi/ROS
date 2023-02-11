using Microsoft.AspNetCore.Mvc;
using ROS.Entity;
using ROS.Interface;
using ROS.Service;
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
    private IAnalyzePurchase _analyzePurchase;

    public PurchaseController(IRepository<Cheque> chequeRepository,
        IRepository<Product> productRepository,
        IShopRepository shopRepository,
        ILog log,
        IAnalyzePurchase analyzePurchase)
    {
        _shopRepository = shopRepository;
        _chequeRepository = chequeRepository;
        _productRepository = productRepository;
        _log = log;
        _analyzePurchase = analyzePurchase;
    }

    [HttpGet]
    [Route("/cheques")]
    public async Task<List<PurchaseInfo>> GetPagePurchaseInfo(int limit, int page)
    {
        var purchaseInfos = new List<PurchaseInfo>();
        _log.Info("начало");
        foreach (var cheque in await _chequeRepository.TakePage(limit, page))
        {
            _log.Info("внутри цикла элементов");
            var shop = await _shopRepository.Get(cheque.ShopId);
            var mainInfo = new PurchaseInfo()
            {
                Id = cheque.Id,
                TotalAmount = cheque.TotalAmount,
                NameShop = shop.NameShop,
                Location = shop.Location
            };
            
            _log.Info("возвращаем");
            purchaseInfos.Add(mainInfo);
        }

        return purchaseInfos;
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

        HttpContext.Response.StatusCode = 201;
        _log.Info("данные загруженны");
        return purchaseInfo.ToPurchase();
    }
}