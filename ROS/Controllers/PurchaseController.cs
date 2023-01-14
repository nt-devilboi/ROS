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
    [Route("/cheques")]
    public async Task<List<Cheque>> Get()
    {
        return await _chequeRepository.ToList();
    }

    [HttpGet]
    [Route("/cheques/cheque")]
    public async Task<Purchase> GetPurchase(Guid guid)
    {
        var cheque = await _chequeRepository.Get(guid);
        var product = await _productRepository.Where(cheque.Id);
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
        HttpContext.Response.StatusCode = 201;
        return purchaseInfo.ToPurchase();
    }
}