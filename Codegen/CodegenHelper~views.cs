namespace Ans.Net7.Common.Codegen
{

	public partial class CodegenHelper
	{

		public void MakeViews(
			string path)
		{
			foreach (var item1 in AllTables)
			{
				var path1 = Path.Combine(SolutionPath, path, $"{item1.NamePluralize}");
				SuppIO.CreateDirectoryIfNotExists(path1);
				SuppIO.FileWrite(Path.Combine(path1, "_viewstart.cshtml"), _getViewStart(item1));
				SuppIO.FileWrite(Path.Combine(path1, "List.cshtml"), _getViewList(item1));
				SuppIO.FileWrite(Path.Combine(path1, "Add.cshtml"), _getViewAdd(item1));
				SuppIO.FileWrite(Path.Combine(path1, "Details.cshtml"), _getViewDetails(item1));
				SuppIO.FileWrite(Path.Combine(path1, "Edit.cshtml"), _getViewEdit(item1));
				SuppIO.FileWrite(Path.Combine(path1, "Delete.cshtml"), _getViewDelete(item1));
			}
		}

	}

}
