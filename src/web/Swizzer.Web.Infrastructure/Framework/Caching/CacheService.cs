using Microsoft.Extensions.Caching.Memory;
using Swizzer.Shared.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Framework.Caching
{
    public interface ICacheService
    {
        void Set<TEntity>(TEntity entity)
            where TEntity : IIdProvider;

        TEntity Get<TEntity>(object key)
            where TEntity : IIdProvider;
    }
    public class CacheService : ICacheService
    {
        private readonly CacheSettings _cacheSettings;
        private readonly IMemoryCache _memoryCache;
        public CacheService(CacheSettings cacheSettings, IMemoryCache memoryCache)
        {
            _cacheSettings = cacheSettings;
            _memoryCache = memoryCache;
        }
        public TEntity Get<TEntity>(object key) where TEntity : IIdProvider
            => _memoryCache.Get<TEntity>(GetKey<TEntity>(key));

        public void Set<TEntity>(TEntity entity) where TEntity : IIdProvider
            => _memoryCache.Set(GetKey<TEntity>(entity.Id), entity, _cacheSettings.Duration);

        private string GetKey<TEntity>(object key)
            => $"{typeof(TEntity)}--{key}";
    }
}
