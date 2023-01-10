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
            ChequeId = cheque.ChequeId, 
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
        return (await _db.cheques.FindAsync(guid))!;
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}