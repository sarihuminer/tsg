using Demo.Domain.Model.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Demo.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Application.Interfaces;

namespace Demo.Repositories.Implementations.Repositories
{
    public class ChachedPersonRepository 
    {
        private readonly IMemoryCache _memoryCache;

        private readonly List<string> _cacheKeys;

        public ChachedPersonRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;

            _cacheKeys = new List<string>();
        }

        public async Task GetOrAddAsync(Person person)
        {
            if (!_cacheKeys.Contains(person.Source))
            {
                _cacheKeys.Add(person.Source);
            }

            await _memoryCache.GetOrCreateAsync(person.Source, entry =>
            {
                return Task.FromResult(person);
            });
        }

 

    }
}
