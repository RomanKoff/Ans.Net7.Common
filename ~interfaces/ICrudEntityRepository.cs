using System.Linq.Expressions;

namespace Ans.Net7.Common
{

    public interface ICrudEntityRepository<TEntity>
    {
        string Name { get; }
        string NamePluralize { get; }

        IQueryable<TEntity> GetItemsAsQueryable(Expression<Func<TEntity, bool>> filter, string order, bool isDescending);
        IEnumerable<TEntity> GetItems(string order, bool isDescending);
        IEnumerable<TEntity> GetItems(int ptr, string order, bool isDescending);
        TEntity GetNew();
        TEntity GetNew(int ptr);
        TEntity GetItem(int id);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void UpdateSelective(TEntity entity, params string[] properties);
        void Remove(TEntity entity);
        void Remove(int id);
        void SaveChanges();
    }

}
