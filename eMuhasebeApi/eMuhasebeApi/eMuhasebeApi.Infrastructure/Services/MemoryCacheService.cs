﻿using eMuhasebeApi.Application.Services;
using Microsoft.Extensions.Caching.Memory;

namespace eMuhasebeApi.Infrastructure.Services;

public sealed class MemoryCacheService(IMemoryCache cache) : ICacheService
{
    public T? Get<T>(string key)
    {
        cache.TryGetValue<T>(key, out var value);
        return value;
    }

    public bool Remove(string key)
    {
        cache.Remove(key);
        return true;
    }

    public void Set<T>(string key, T value, TimeSpan? expiry = null)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromHours(1),
        };
        cache.Set(key, value, cacheEntryOptions);
    }

    public void RemoveAll()
    {
        List<string> keys = new List<string>()
        {
            "cashRegisters",
            "banks",
            "invoices",
            "products",
            "customers"
        };
        foreach (var key in keys)
        {
            cache.Remove(key);
        }
    }
}