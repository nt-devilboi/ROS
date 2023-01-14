using Microsoft.EntityFrameworkCore;
using ROS.DataBase;
using ROS.Entity;

namespace ROS;

public class ChequeRepository : IRepository<Cheque> 
{
    private PurchaseContext _db = new PurchaseContext();

    public void Add(Cheque cheque)
    {
        _db.cheques.Add(new Cheque()
        {
            Id = cheque.Id,
            ShopId = cheque.ShopId,
            Time = cheque.Time,
            TotalAmount = cheque.TotalAmount
        });
    }
    

    public async Task<List<Cheque>> ToList()
    {
        return _db.cheques.ToList();
    }

    public async Task<Cheque> Get(Guid guid)
    {
        return (await _db.cheques.FirstOrDefaultAsync(cheque => cheque.Id == guid))!;
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }

    public Task<Cheque[]> Where(Guid element)
    {
        throw new NotImplementedException();
    }

    public Task<Cheque> GetFirst(Guid element)
    {
        throw new NotImplementedException();
    }
}