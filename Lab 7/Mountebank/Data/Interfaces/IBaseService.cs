namespace Mountebank.Data.Interfaces;

public interface IBaseService<T>
{
    public T GetById(int id);
    public T GetByName(string name);
    public IEnumerable<T> GetAll();
}