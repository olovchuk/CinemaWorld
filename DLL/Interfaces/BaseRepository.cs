﻿using System.Linq.Expressions;
using DLL.Context;
using Microsoft.EntityFrameworkCore;

namespace DLL.Interfaces {
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class {
        private            DbSet<TEntity> _entities;
        protected readonly CinemaContext  CinemaContext;
        protected          DbSet<TEntity> Entities => this._entities;
        protected BaseRepository(CinemaContext context) {
            CinemaContext = context;
            _entities = CinemaContext.Set<TEntity>();
        }
        public virtual async Task<IReadOnlyCollection<TEntity>> GetAllAsync() {
            return await this._entities.ToListAsync().ConfigureAwait(false);
        }
        public virtual async Task<IReadOnlyCollection<TEntity>> FindByConditionAsync(
                Expression<Func<TEntity, bool>> predicate) {
            return await this._entities.Where(predicate).ToListAsync().ConfigureAwait(false);
        }
        public virtual async Task InsertAsync(TEntity data) {
            await this._entities.AddAsync(data).ConfigureAwait(false);
            await CinemaContext.SaveChangesAsync();
        }
    }
}