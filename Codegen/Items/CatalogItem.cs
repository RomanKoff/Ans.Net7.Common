using Ans.Net7.Common.Codegen.Schema;

namespace Ans.Net7.Common.Codegen.Items
{

	public class CatalogItem
	{

		/* ctor */


		public CatalogItem(
			CatalogXmlElement catalog)
		{
			Name = catalog.Name;
			Title = catalog.Title ?? Name;
			Remark = catalog.Remark ?? "";
			_scan(catalog.Entities, null, 0);
		}


		/* readonly properties */


		public string Name { get; private set; }
		public string Title { get; private set; }
		public string Remark { get; private set; }

		public List<TableItem> AllTables { get; private set; } = new();

		public IEnumerable<TableItem> TopTables
			=> AllTables.Where(x => x.Level == 0);


		/* privates */


		private void _scan(
			IEnumerable<EntityXmlElement> entities,
			TableItem master,
			int level)
		{
			foreach (var item1 in entities)
			{
				var table1 = new TableItem(
					this, master, item1, _getTableName(master, item1.Name), level);
				AllTables.Add(table1);
				level++;
				foreach (var item2 in item1.Manyrefs)
				{
					var table2 = new TableItem(this, table1, item2, level);
					AllTables.Add(table2);
				}
				_scan(item1.Entities, table1, level);
				level--;
			}
		}


		private string _getTableName(
			string name)
			=> $"{Name}{name}";


		private string _getTableName(
			TableItem master,
			string name)
			=> (master != null)
				? $"{master.Name}{name}" : _getTableName(name);

	}

}