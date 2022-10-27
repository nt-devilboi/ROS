using ROS.DataBase;
using ROS.Entity;

namespace ROS;

public class ChequeRepository : IChequeRepository
{
    private ChequeContext _db = new ChequeContext();

    public void Create(string shopId, double totalAmount, DateTime time)
    {
        _db.Cheques.Add(new Cheque()
        {
            ShopId = shopId, TotalAmount = totalAmount, Time = time
        });
    }
    

    public async Task<List<Cheque>> ToList()
    {
        return _db.Cheques.ToList();
    }

    public async Task<Cheque> Get(Guid guid)
    {
        return (await _db.Cheques.FindAsync(guid))!;
    }

    public async void SaveChanges()
    {
        _db.SaveChanges();
    }
}