using Demo.Domain.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repositories.Implementations.Repositories
{
    public interface ICacheRepository<T> where T : BaseEntity
    {
        Task<T> CacheEntityAsync(T entity);

        T MergeCachedEntities();
    }
}
