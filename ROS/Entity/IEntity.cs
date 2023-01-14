namespace ROS.Entity;

public interface IEntity<T>
{
    public T Id { get; set; }
}