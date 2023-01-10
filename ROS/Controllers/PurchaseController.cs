using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ROS.Entity;
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

    private ILog _log;

    public PurchaseController(IRepository<Cheque> chequeRepository, IRepository<Product> productRepository, ILog log)
    {
        _chequeRepository = chequeRepository;
        _productRepository = productRepository;
        _log = log;
    }

    [HttpPost]
    [Route("/addCheque")]
    public async Task<List<Cheque>> AddFakeInBd()
    {
        var list = TemporaryBd.TemporaryBd.GetSimpleCheque();
        foreach (var cheque in list)
        {
            _chequeRepository.Add(cheque);
            _chequeRepository.SaveChanges();
        }

        return list;
    }


    [HttpGet]
    [Route("/getcheque")]
    public async Task<List<Cheque>> Get()
        => await _chequeRepository.ToList();


    [HttpPost]
    [Route("/newPurchase")]
    public async Task<PurchaseResponse> AddPurchase([FromBody] InfoPurchaseRequest infoPurchase)
    {
        var purchase = new Purchase(infoPurchase);
        _chequeRepository.Add(purchase.Cheque);
        
        foreach (var product in purchase.Products) // написать отельный mildware для работы с репозиториями
        {
            _productRepository.Add(product);
            _productRepository.SaveChanges();
        }
        _chequeRepository.SaveChanges();
        _log.Info("complete");

        return new PurchaseResponse()
            { Shop = purchase.Shop, Cheque = purchase.Cheque, Products = purchase.Products};
    }
}