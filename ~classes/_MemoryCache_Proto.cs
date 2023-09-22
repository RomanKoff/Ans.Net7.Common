using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace Ans.Net7.Common
{

    public abstract class _MemoryCache_Proto<T>
	{

		private readonly IMemoryCache _cache;


		/* abstracts */


		public abstract T GetObject();


		/* ctors */


		public _MemoryCache_Proto(
			IMemoryCache cache,
			string cacheKey,
			MemoryCacheEntryOptions cacheOptions)
		{
			_cache = cache;
			CacheKey = cacheKey;
			CacheOptions = cacheOptions;
		}


		public _MemoryCache_Proto(
			IMemoryCache cache,
			string cacheKey,
			int slidingExpirationSeconds,
			int absoluteExpirationRelativeToNowSeconds)
			: this(cache, cacheKey, new MemoryCacheEntryOptions())
		{
			if (slidingExpirationSeconds > 0)
				CacheOptions.SlidingExpiration = TimeSpan.FromSeconds(
					slidingExpirationSeconds);
			if (absoluteExpirationRelativeToNowSeconds > 0)
				CacheOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(
					absoluteExpirationRelativeToNowSeconds);
		}


		/* readonly properties */


		public string CacheKey { get; private set; }
		public MemoryCacheEntryOptions CacheOptions { get; private set; }


		/* methods */


		public void Clear()
		{
			_cache.Remove(CacheKey);
		}


		/* functions */


		public T Get()
		{
			if (!_cache.TryGetValue(CacheKey, out T value1))
			{
				value1 = GetObject();
				_cache.Set(CacheKey, value1, CacheOptions);
				Debug.WriteLine($"[Ans.Net7.Common._MemoryCache_Proto] Set memory cache {CacheKey}.");
			}
			return value1;
		}


		public T Refresh()
		{
			Clear();
			return Get();
		}

	}

}
