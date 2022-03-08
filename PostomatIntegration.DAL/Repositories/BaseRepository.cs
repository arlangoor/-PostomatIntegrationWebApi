using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using AttributeExtentions;
using Microsoft.EntityFrameworkCore.Storage;

namespace PostomatIntegration.DAL.Repositories
{
	[RegisterService]
	public class BaseRepository<TEntity> where TEntity : class
	{
		public BaseRepository(DbSet<TEntity> dbSet)
		{
			DbSet = dbSet;
			DbContext = GetDbContext(dbSet);
		}
		protected DbContext DbContext { get; private set; }
		public async Task<IDbContextTransaction> BeginTransactionAsync()
		{
			return await DbContext.Database.BeginTransactionAsync();
		}
		protected DbSet<TEntity> DbSet { get; private set; }
		public IQueryable<TEntity> Query => DbSet.Select(x => x);
		public async Task<List<TEntity>> ExecuteQueryAsync(IQueryable<TEntity> query)
			=> await query.ToListAsync();
		public virtual async Task<TEntity> InsertAsync(TEntity entity)
		{
			DbSet.Add(entity);
			await DbContext.SaveChangesAsync();
			DbContext.Entry(entity).State = EntityState.Detached;
			return entity;
		}
		public virtual async Task UpdateAsync(TEntity entity)
		{
			DbContext.Entry(entity).State = EntityState.Modified;
			await DbContext.SaveChangesAsync();
			DbContext.Entry(entity).State = EntityState.Detached;
		}
		public virtual async Task DeleteAsync(TEntity entity)
		{
			DbSet.Remove(entity);
			await DbContext.SaveChangesAsync();
			DbContext.Entry(entity).State = EntityState.Detached;
		}
		private static DbContext GetDbContext<T>(DbSet<T> dbSet) where T : class
		{
			var infrastructure = dbSet as IInfrastructure<IServiceProvider>;
			var serviceProvider = infrastructure.Instance;
			var currentDbContext = serviceProvider.GetService(typeof(ICurrentDbContext))
									   as ICurrentDbContext;
			return currentDbContext.Context;
		}
	}
}
