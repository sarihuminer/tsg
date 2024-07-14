using Demo.Domain.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repositories.Implementations.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task CacheEntityAsync(T entity);
        T MergeCachedEntities();
    }
}
