namespace Ans.Net7.Common
{

	public interface IEntity
	{
		int Id { get; set; }
	}



	public interface IEntitySlave
		: IEntity
	{
		int? ItemMasterPtr { get; set; }
	}

}
