using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ans.Net7.Common.Crud
{

	public interface ISlaveEntity
		: IMasterEntity
	{
		int MasterPtr { get; set; }
	}



	public interface ICrudSlaveRepository<TSlaveEntity>
		: ICrudRepository<TSlaveEntity>
		where TSlaveEntity : class, ISlaveEntity
	{
		IEnumerable<TSlaveEntity> GetItems(
			Expression<Func<TSlaveEntity, bool>> filter, int ptr, string order, bool isDescending);
		TSlaveEntity GetNew(int ptr);
	}



	public abstract class _CrudSlaveRepository_Proto<TSlaveEntity>
		: _CrudRepository_Base<TSlaveEntity>,
		ICrudSlaveRepository<TSlaveEntity>
		where TSlaveEntity : class, ISlaveEntity
	{

		/* ctor */


		public _CrudSlaveRepository_Proto(
			DbContext db)
			: base(db)
		{
		}


		/* functions */


		public virtual IEnumerable<TSlaveEntity> GetItems(
			Expression<Func<TSlaveEntity, bool>> filter,
			int ptr,
			string order,
			bool isDescending)
		{
			if (order == null)
			{
				order = DefaultOrder ?? "Id";
				isDescending = IsDefaultOrderDescending;
			}
			if (filter == null)
				filter = x => x.MasterPtr == ptr;
			else
				filter.And(x => x.MasterPtr == ptr);
			return GetItemsAsQueryable(
				filter, order, isDescending)
					.ToList();
		}


		public abstract TSlaveEntity GetNew(int ptr);

	}

}
