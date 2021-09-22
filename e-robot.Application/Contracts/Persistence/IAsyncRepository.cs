using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace e_robot.Application.Contracts.Persistence
{
    public interface IAsyncRepository 
    {
        Task<TDest> GetByIdAsync<TEntity, TDest>(object id) where TEntity : class;

        Task<TEntity> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "") where TEntity : class;

        Task<List<TDest>> GetAllAsync<TEntity, TDest>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "") where TEntity : class;
        Task<TEntity> InsertAsync<TEntity, TDest>(TDest dest, dynamic obj = null) where TEntity : class;
        Task<TEntity> InsertAsync<TEntity>(TEntity entity) where TEntity : class;
        Task UpdateSimpleAsync<TEntity>(TEntity dest) where TEntity : class;
        Task<List<TEntity>> InsertRangeAsync<TEntity, TDest>(List<TDest> dest) where TEntity : class;
        Task<TEntity> UpdateAsync<TEntity, TDest>(TDest dest, string ignores = null, dynamic obj = null) where TEntity : class;
        Task UpdateRangeAsync<TEntity, TDest>(List<TDest> dest) where TEntity : class;
        Task DeleteAsync<TEntity>(object id) where TEntity : class;
        Task DeleteRangeWithFilterAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class;
        Task<TEntity> UpdateDeletedAsync<TEntity>(object id, dynamic obj = null) where TEntity : class;

        Task SaveAsync();
        void Save();

    }
}
