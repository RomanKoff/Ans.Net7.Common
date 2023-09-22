using Ans.Net7.Common.Codegen.Schema;

namespace Ans.Net7.Common.Codegen.Gen
{

	public class CatalogItem
	{

		/* properties */


		public string Name { get; set; }
		public string Title { get; set; }
		public string Remark { get; set; }



		/* readonly properties */


		public List<TableItem> AllTables { get; private set; } = new();

		public IEnumerable<TableItem> TopTables
			=> AllTables.Where(x => x.Level == 0);



		/* ctors */


		public CatalogItem(
		   CatalogXmlElement catalog)
		{
			Name = catalog.Name;
			Title = catalog.Title ?? Name;
			Remark = catalog.Remark ?? "";
			_scan(catalog.Entities, null, 0);
		}



		/* privates */


		private void _scan(
			IEnumerable<EntityXmlElement> entities,
			TableItem master,
			int level)
		{
			foreach (var entity1 in entities)
			{
				var table1 = new TableItem(
					this, master, entity1, _getTableName(master, entity1.Name), level);
				AllTables.Add(table1);
				level++;
				foreach (var mr1 in entity1.Manyrefs)
				{
					var table2 = new TableItem(this, table1, mr1, level);
					AllTables.Add(table2);
				}
				_scan(entity1.Entities, table1, level);
				level--;
			}
		}


		private string _getTableName(
			string name)
		{
			return $"{Name}{name}";
		}


		private string _getTableName(
			TableItem master,
			string name)
		{
			return master != null
				? $"{master.Name}{name}"
				: _getTableName(name);
		}

	}

}