using System.Text;

namespace Ans.Net7.Common.Codegen
{

	public partial class CodegenHelper
	{

		public void MakeDbInit()
		{
			var path1 = Path.Combine(DalPath);
			SuppIO.CreateDirectoryIfNotExists(path1);
			var filename1 = Path.Combine(path1, $"{ContextName}DbInit.cs");
			SuppIO.FileWrite(filename1, _getDbInit());
		}


		/* privates */


		/* ==================================================================== */
		private string _getDbInit()
		{
			var sb1 = new StringBuilder();
			sb1.Append(_getCSharpAttention());
			sb1.Append(@$"
using Ans.Net7.Psql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace {DalNamespace}
{{

	public static partial class {ContextName}DbInit
    {{

		public static void {ContextName}DbPrepare(
			this IHost host,
			bool useInitialize)
		{{
			using var scope1 = host.Services.CreateScope();
			var provider1 = scope1.ServiceProvider;
			var context1 = provider1.GetRequiredService<{ContextName}DbContext>();
			if (context1.Database.EnsureCreated())
			{{
				Debug.WriteLine(""[{DalNamespace}.{ContextName}DbInit] Prepare Db"");
{_getDateUpdateTriggers()}
				if (useInitialize)
					Initialize(context1);
			}}
		}}


		public static void Initialize(
			{ContextName}DbContext context)
		{{
			if (context.{AllTables.First().NamePluralize}.Any())
				return;
			Debug.WriteLine(""[{DalNamespace}.{ContextName}DbInit] Initialize Db"");
		}}

	}}

}}");
			return sb1.ToString();
		}
		/* ==================================================================== */





		/* -------------------------------------------------------------------- */
		private string _getDateUpdateTriggers()
		{
			var sb1 = new StringBuilder();
			foreach (var item1 in AllTables.Where(x => x.UseTimestamp))
			{
				sb1.Append(@$"
				context1.CreateTrigger_DateUpdate(""{item1.NamePluralize}"");");
			}
			if (sb1.Length > 0)
			{
				sb1.Insert(0, @"
				// timestamp update triggers
				context1.CreateFunction_DateUpdate();");
				sb1.AppendLine();
			}
			return sb1.ToString();
		}
		/* -------------------------------------------------------------------- */

	}

}
