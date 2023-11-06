using System.Reflection;

namespace Ans.Net7.Common
{

	public static class SuppApp
	{

		/*
		 * string CurrentPath { get; }
		 * string ProjectPath { get; }
		 * string SolutionPath { get; }
		 * 
		 * string GetName();
		 * string GetVersion();
		 * string GetDescription();
		 */


		public static string CurrentPath
			=> _currentPath ??= Environment.CurrentDirectory;
		private static string _currentPath = null;


		public static string ProjectPath
			=> _projectPath ??= Directory.GetParent(Environment.CurrentDirectory)
				.Parent.Parent.FullName;
		private static string _projectPath = null;


		public static string SolutionPath
			=> _solutionPath ??= Directory.GetParent(Environment.CurrentDirectory)
				.Parent.Parent.Parent.FullName;
		private static string _solutionPath = null;


		public static string GetName()
		{
			return Assembly.GetCallingAssembly()
				.GetName().Name;
		}


		public static string GetVersion()
		{
			return Assembly.GetCallingAssembly()
				.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
					.InformationalVersion;
		}


		public static string GetDescription()
		{
			return Assembly.GetCallingAssembly()
				.GetCustomAttribute<AssemblyDescriptionAttribute>()?
					.Description;
		}

	}

}
