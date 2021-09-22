using AutoMapper;
using e_robot.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace e_robot.Infrastructure.Repositories.EntityFramework
{
    public class GenericMapperRepository : IAsyncRepository
    {
        internal DbContext context;
        internal IMapper mapper;
        internal IHostingEnvironment hostingEnvironment;

        public GenericMapperRepository(DbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public GenericMapperRepository(DbContext context, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.mapper = mapper;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<TDest> GetByIdAsync<TEntity, TDest>(object id) where TEntity : class
        {
            return mapper.Map<TDest>(await context.Set<TEntity>().FindAsync(id));
        }


        public async Task<List<TDest>> GetAllAsync<TEntity, TDest>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "") where TEntity : class
        {
            DbSet<TEntity> dbSet = context.Set<TEntity>();
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return mapper.Map<List<TDest>>(await orderBy(query).AsNoTracking().ToListAsync());
            }
            else
            {
                return mapper.Map<List<TDest>>(await query.AsNoTracking().ToListAsync());
            }
        }


        public async Task<TEntity> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "") where TEntity : class
        {
            DbSet<TEntity> dbSet = context.Set<TEntity>();
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }


            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<TEntity> InsertAsync<TEntity, TDest>(TDest dest, dynamic obj = null) where TEntity : class
        {

            TEntity entity = mapper.Map<TEntity>(dest);

            if (obj != null)
            {
                var t = typeof(TEntity);

                var str = obj.GetType().GetProperties();

                foreach (var k in str)
                {
                    dynamic prop = t.GetProperty(k.Name);
                    dynamic value = obj.GetType().GetProperty(k.Name).GetValue(obj, null);
                    prop.SetValue(entity, value);
                }
            }

            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<TEntity>> InsertRangeAsync<TEntity, TDest>(List<TDest> dest) where TEntity : class
        {
            List<TEntity> entity = mapper.Map<List<TEntity>>(dest);
            await context.AddRangeAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync<TEntity, TDest>(TDest dest, string ignores = null, dynamic obj = null) where TEntity : class
        {
            TEntity newEntity = mapper.Map<TEntity>(dest);

            if (ignores != null)
            {
                TEntity oldEntity = await context.Set<TEntity>().FindAsync(newEntity.GetType().GetProperty("Id").GetValue(newEntity, null));


                var t = typeof(TEntity);

                if (obj != null)
                {
                    var str = obj.GetType().GetProperties();

                    foreach (var k in str)
                    {
                        dynamic prop = t.GetProperty(k.Name);
                        dynamic value = obj.GetType().GetProperty(k.Name).GetValue(obj, null);
                        prop.SetValue(newEntity, value);
                    }
                }

                foreach (var ignore in ignores.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    dynamic prop = t.GetProperty(ignore);
                    dynamic value = oldEntity.GetType().GetProperty(ignore).GetValue(oldEntity);
                    prop.SetValue(newEntity, value);
                }

                context.Entry(oldEntity).State = EntityState.Detached;
            }

            context.Update(newEntity);
            await context.SaveChangesAsync();
            return newEntity;
        }

        public async Task UpdateRangeAsync<TEntity, TDest>(List<TDest> dest) where TEntity : class
        {
            List<TEntity> entityToUpdate = mapper.Map<List<TEntity>>(dest);
            context.UpdateRange(entityToUpdate);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync<TEntity>(object id) where TEntity : class
        {
            TEntity entityToDelete = await context.Set<TEntity>().FindAsync(id);
            context.Remove(entityToDelete);
            await context.SaveChangesAsync();
        }

        public async Task DeleteRangeWithFilterAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            List<TEntity> entityToDelete = await query.ToListAsync();
            context.RemoveRange(entityToDelete);
            await context.SaveChangesAsync();
        }


        public async Task<TEntity> UpdateDeletedAsync<TEntity>(object id, dynamic obj = null) where TEntity : class
        {
            TEntity newEntity = await context.Set<TEntity>().FindAsync(id);

            var t = typeof(TEntity);

            if (obj != null)
            {
                var str = obj.GetType().GetProperties();

                foreach (var k in str)
                {
                    dynamic prop = t.GetProperty(k.Name);
                    dynamic value = obj.GetType().GetProperty(k.Name).GetValue(obj, null);
                    prop.SetValue(newEntity, value);
                }
            }
            context.Entry(newEntity).State = EntityState.Detached;

            context.Update(newEntity);
            await context.SaveChangesAsync();
            return newEntity;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task UpdateSimpleAsync<TEntity>(TEntity dest) where TEntity : class
        {
            context.Update(dest);
            await context.SaveChangesAsync();
        }

        public async Task<TEntity> InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            return entity;
        }
    }
}