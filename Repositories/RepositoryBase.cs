using Microsoft.EntityFrameworkCore;
using skill_test_1.Data;
using skill_test_1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace skill_test_1.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> 
        where T : class
    {
        private stest1Context context;
        
        public RepositoryBase(stest1Context repositoryContext)
        {
            this.context = repositoryContext;
        }

        public async Task<T> Add(T entity)
        {
            context.Set<T>().Add(entity);   
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(int id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> Get(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            var result =  await context.Set<T>().Where(expression).ToListAsync();
            return result.AsQueryable();
        }




    }
}
