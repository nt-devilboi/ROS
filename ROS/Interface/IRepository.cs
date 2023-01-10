using ROS.Entity;

namespace ROS;

public interface IRepository<T>
{
    public void Add(T entity);

    public Task<List<T>> ToList();
    public Task<T> Get(Guid guid);
    public void SaveChanges();

}