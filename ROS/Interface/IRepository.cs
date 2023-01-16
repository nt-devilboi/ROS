using ROS.Entity;

namespace ROS;

public interface IRepository<T>
{
    public void Add(T entity);

    public Task<T> Get(Guid id);
    
    public Task<List<T>> ToList();

    public Task<T[]> Where(Guid chequeId);

    public void SaveChanges();
}