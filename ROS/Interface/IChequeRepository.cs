using ROS.Entity;

namespace ROS;

public interface IChequeRepository
{
    public void Create(string shopId, double totalAmount, DateTime time);

    public Task<List<Cheque>> ToList();
    public Task<Cheque> Get(Guid guid);
    public void SaveChanges();

}