using ETicaret.Data;
using ETicaret.Data.Entities;
using ETicaret.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Services.Concrete
{
    public class EFRepository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly ETicaretDbContext _dbcontext;

        public EFRepository(ETicaretDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = _dbcontext.Set<T>().Where(x=>x.IsActive || !x.IsActive);
            if (include != null)
            {
                query = include(query);
            }
            return query.ToList();
        }

        public T Get(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = _dbcontext.Set<T>().AsQueryable();
            if (include != null)
            {
                query = include(query);
            }
            return query.FirstOrDefault(x => x.Id == id);
        }

        public bool Create(T entity)
        {

            entity.CreatedDate = DateTime.Now;
            entity.IsActive = true;
            entity.CreatedById = 1;
            _dbcontext.Set<T>().Add(entity);
            return _dbcontext.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {
            _dbcontext.Set<T>().Update(entity);
            _dbcontext.Entry<T>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return _dbcontext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var entity = Get(id);
            entity.IsActive = false;
            entity.UpdatedDate = DateTime.Now;
            return Update(entity);
        }

        public List<T> GetAllUnactives(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = _dbcontext.Set<T>().Where(x => !x.IsActive);
            if (include != null)
            {
                query = include(query);
            }
            return query.ToList();
        }
    }
}
