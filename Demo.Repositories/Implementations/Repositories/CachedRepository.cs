﻿using Demo.Application.DTOs;
using Demo.Domain.Model.Data;
using Elfie.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repositories.Implementations.Repositories
{
    public class CachedRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly IMemoryCache _memoryCache;
        private readonly List<string> _cacheKeys;
        private readonly PrioritySettings _prioritySettings;

        public CachedRepository(IMemoryCache memoryCache, string prioritySettingsFile)
        {
            _memoryCache = memoryCache;
            _cacheKeys = new List<string>();
            _prioritySettings = LoadPrioritySettings(prioritySettingsFile);
        }

        private PrioritySettings LoadPrioritySettings(string filePath)
        {
            var json = System.IO.File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<PrioritySettings>(json);
        }

        public async Task CacheEntityAsync(T entity)
        {
            if (!_cacheKeys.Contains(entity.Source))
            {
                _cacheKeys.Add(entity.Source);
            }

            await _memoryCache.GetOrCreateAsync(entity.Source, entry =>
            {
                //entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5); // Optional cache expiration
                return Task.FromResult(entity);
            });
        }

        public T MergeCachedEntities()
        {
            var mergedEntity = new T();

            foreach (var field in _prioritySettings.Priorities)
            {
                foreach (var source in field.Value)
                {
                    if (_memoryCache.TryGetValue(source, out T entity))
                    {
                        var value = GetValue(entity, field.Key);
                        if (value != null)
                        {
                            SetValue(mergedEntity, field.Key, value);
                            break; // Move to the next field after finding the highest priority value
                        }
                    }
                }
            }

            return mergedEntity;
        }

        private object GetValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName)?.GetValue(obj, null);
        }

        private void SetValue(object obj, string propertyName, object value)
        {
            obj.GetType().GetProperty(propertyName)?.SetValue(obj, value);
        }
    }
}
