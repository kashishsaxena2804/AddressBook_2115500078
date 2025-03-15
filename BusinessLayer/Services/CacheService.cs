using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using BusinessLayer.Interfaces;



namespace BusinessLayer.Services
{
  
    public class CacheService : ICacheService
    {
        private readonly IDatabase _cache;

        public CacheService(IConnectionMultiplexer redis)
        {
            _cache = redis.GetDatabase();
        }

        public void SetData(string key, string value, TimeSpan expiry)
        {
            _cache.StringSet(key, value, expiry);
        }

        public string GetData(string key)
        {
            return _cache.StringGet(key);
        }

        public void RemoveData(string key)
        {
            _cache.KeyDelete(key);
        }
    }

}
