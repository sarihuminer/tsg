using Demo.Application.Interfaces.Repository.Repositories;
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

namespace Demo.Repositories.Implementations.Repositories
{
    public class ChachedPersonRepository : IPersonRepository
    {
        private readonly IMemoryCache _memoryCache;

        private readonly List<string> _cacheKeys;

        public ChachedPersonRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;

            _cacheKeys = new List<string>();
        }

        public async Task GetOrAddAsync(PersonRequestDto person)
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

        public PersonResponseDto GetMergeCachedPersons()
        {
            var mergedPerson = new PersonResponseDto();

            foreach (var key in _cacheKeys)
            {
                if (_memoryCache.TryGetValue(key, out PersonRequestDto person))
                {
                    if (string.IsNullOrEmpty(mergedPerson.Name))
                    {
                        mergedPerson.Name = person.Name;
                    }

                    if (mergedPerson.Age == 0)
                    {
                        mergedPerson.Age = person.Age;
                    }

                    // Continue merging other fields
                }
            }

            return mergedPerson;
        }

    }
}
