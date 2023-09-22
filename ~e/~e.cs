using Microsoft.Extensions.DependencyInjection;

namespace Ans.Net7.Common
{

	public static partial class _e
	{

		/*
         * IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source);
         * bool HasService(this IServiceCollection services, Type type)
         */


		/// <summary>
		/// foreach (var (item1, i1) in data1.WithIndex())
		/// </summary>
		public static IEnumerable<(T item, int index)> WithIndex<T>(
			this IEnumerable<T> source)
		{
			return source.Select((item, index) => (item, index));
		}


		public static bool HasService(
			this IServiceCollection services,
			Type type)
		{
			return services.Any(x => x.ServiceType == type);
		}

	}

}
