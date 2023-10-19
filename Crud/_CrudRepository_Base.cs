using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ans.Net7.Common.Crud
{

	public interface ICrudRepository<TEntity>
		where TEntity : class
	{
		string DefaultOrder { get; set; }
		bool IsDefaultOrderDescending { get; set; }

		DbContext DbContext { get; }
		DbSet<TEntity> DbSet { get; }

		IQueryable<TEntity> GetItemsAsQueryable(Expression<Func<TEntity, bool>> filter, string order, bool isDescending);
		TEntity GetItem(int id);

		void Add(TEntity entity);
		void Update(TEntity entity);
		void UpdateSelective(TEntity entity, params string[] properties);
		void Remove(int id);
		void Remove(TEntity entity);
	}



	public class _CrudRepository_Base<TEntity>
		: ICrudRepository<TEntity>
		where TEntity : class
	{

		/* ctor */


		public _CrudRepository_Base(
			DbContext db)
		{
			DbContext = db;
			DbSet = db.Set<TEntity>();
		}


		/* properties */


		public string DefaultOrder { get; set; }
		public bool IsDefaultOrderDescending { get; set; }


		/* readonly properties */


		public DbContext DbContext { get; private set; }
		public DbSet<TEntity> DbSet { get; private set; }


		/* functions */


		public virtual IQueryable<TEntity> GetItemsAsQueryable(
			Expression<Func<TEntity, bool>> filter,
			string order,
			bool isDescending)
		{
			var query1 = filter == null
				? DbSet.AsQueryable()
				: DbSet.Where(filter);
			if (!string.IsNullOrEmpty(order))
				query1 = isDescending
					? query1.OrderByDescending(order)
					: query1.OrderBy(order);
			return query1;
		}


		public virtual TEntity GetItem(
			int id)
		{
			var item = DbSet.Find(id);
			return item;
		}


		/* methods */


		public virtual void Add(
			TEntity entity)
		{
			DbSet.Add(entity);
		}


		public virtual void Update(
			TEntity entity)
		{
			DbSet.Attach(entity);
			DbContext.Entry(entity).State = EntityState.Modified;
		}


		public virtual void UpdateSelective(
			TEntity entity,
			params string[] properties)
		{
			DbSet.Attach(entity);
			foreach (var item1 in properties)
				DbSet.Entry(entity).Property(item1).IsModified = true;
		}


		public virtual void Remove(
			int id)
		{
			var entity1 = DbSet.Find(id);
			Remove(entity1);
		}


		public virtual void Remove(
			TEntity entity)
		{
			if (DbContext.Entry(entity).State == EntityState.Detached)
				DbSet.Attach(entity);
			DbSet.Remove(entity);
		}

	}

}
