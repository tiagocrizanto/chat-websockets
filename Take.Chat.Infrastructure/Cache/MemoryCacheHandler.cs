//using Microsoft.Extensions.Caching.Memory;
//using System;

//namespace Take.Chat.Infrastructure.Cache
//{
//    public class MemoryCacheHandler : ICacheHandler
//    {
//        private IMemoryCache MemoryCache { get; }

//        public MemoryCacheHandler(IMemoryCache memoryCache)
//        {
//            MemoryCache = memoryCache;
//        }

//        public T TryGetValue<T>(string key, Func<T> whenNotFound)
//        {
//            if (MemoryCache.TryGetValue(key, out T toReturn)) return toReturn;
//            if (whenNotFound != null) toReturn = whenNotFound();

//            // Set cache options
//            var cacheEntryOptions = new MemoryCacheEntryOptions()
//            // Expiração ao fim do dia
//            .SetSlidingExpiration(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59) - DateTime.Now)
//            .SetAbsoluteExpiration(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59) - DateTime.Now);

//            MemoryCache.Set(key, toReturn);
//            return toReturn;
//        }

//        public T TryGetGlobalCache<T>(string key)
//        {
//            if (MemoryCache.TryGetValue(key, out T toReturn)) return toReturn;

//            return default(T);
//        }

//        public void RemoveGlobalCache(string key)
//        {
//            MemoryCache.Remove(key);
//        }
//    }
//}
