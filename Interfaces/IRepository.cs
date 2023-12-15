using System.Linq.Expressions;

namespace SparkSwim.GoodsService;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> condition);
    Task<T> FindByIdAsync(Guid id);
    Task<T> FindByTitleAsync(string title);
    void Create(IEnumerable<T> entities);
    void Update(IEnumerable<T> entities);
    void SoftDelete(IEnumerable<T> entities);
    void HardDelete(IEnumerable<T> entities);
    void SaveChanges();
}