using Microsoft.AspNetCore.Mvc;
using ROS.Entity;
using ROS.Services;
using Vostok.Logging.Abstractions;

namespace ROS.Controllers;

[ApiController]
[Route("[controller]")]
public class ChequeController : ControllerBase
{
    private IChequeRepository _chequeRepository;
    private ILog _log;

    public ChequeController(IChequeRepository chequeContext, ILog log)
    {
        _chequeRepository = chequeContext;
        _log = log;
    }

    [HttpPost]
    [Route("/newPurchase")]
    public async Task<Result<PurchaseResponse>> AddPurchase([FromBody] InfoPurchaseRequest infoPurchase)
    {
        var purchase = new Purchase(infoPurchase);
        var cheque = purchase.Cheque;

        _chequeRepository.Create(cheque.ShopId, cheque.TotalAmount, cheque.Time);
        _chequeRepository.SaveChanges();
        _log.Info("complete");
        return new Result<PurchaseResponse>
        {
            Value = new PurchaseResponse()
                { Shop = purchase.Shop, Cheque = purchase.Cheque, Products = purchase.Products }
        };
    }
}