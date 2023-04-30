using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Services.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        T Get(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        List<T> GetAllUnactives(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(int id);
    }
}
