using eMuhasebeApi.Application.Services;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace eMuhasebeApi.Infrastructure.Services;

public sealed class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public T? Get<T>(string key)
    {
        var value = _database.StringGet(key);
        if (value.HasValue)
        {
            return JsonConvert.DeserializeObject<T>(value.ToString());
        }
        return default(T?);
    }

    public bool Remove(string key)
    {
        return _database.KeyDelete(key);
    }

    public void Set<T>(string key, T value, TimeSpan? expiry = null)
    {
        var serializedValue = JsonConvert.SerializeObject(value);
        _database.StringSet(key, serializedValue, expiry);
    }
}
