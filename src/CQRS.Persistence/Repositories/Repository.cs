using CQRS.Domain.Abstraccions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CQRS.Persistence.Repositories
{

    internal abstract class Repository<TEntity>
    where TEntity : Entity
    {
        protected readonly CQRSDbContext dbContext;

        protected Repository(CQRSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TEntity?> ObtenerPorIdAsync(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            return await dbContext.Set<TEntity>()
            .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
        }


        public async Task<TEntity?> ObtenerPorFiltro(
            Expression<Func<TEntity, bool>> predicado,
            string? relaciones = null,
            CancellationToken cancellationToken = default
        )
        {
            var query = dbContext.Set<TEntity>()
                .Where(predicado)
                .AsQueryable();

            if (!string.IsNullOrEmpty(relaciones))
            {
                query = relaciones.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, tabla) => current.Include(tabla));
            }


            return await query.FirstOrDefaultAsync();
        }


        public async Task<TEntity?> ObtenerPorIdAsync(
            Guid id, string relaciones, CancellationToken cancellationToken
        )
        {
            var query = dbContext.Set<TEntity>()
                .Where(p => p.Id == id)
                .AsQueryable();

            if (!string.IsNullOrEmpty(relaciones))
            {
                query = relaciones.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, tabla) => current.Include(tabla));
            }


            return await query.FirstOrDefaultAsync();
        }


        public virtual async Task AgregarAsync(TEntity entity)
        {
            await dbContext.AddAsync(entity);
        }

        public void Actualizar(TEntity entity)
        {
            dbContext.Update(entity);
        }

        public void Eliminar(TEntity entity)
        {
            dbContext.Remove(entity);
        }


        public virtual async Task<ICollection<TEntity>> ListarAsync(
            Expression<Func<TEntity, bool>> predicado, bool tracking = false, string? relaciones = null)
        {
            var query = dbContext.Set<TEntity>()
                .Where(predicado)
                .AsQueryable();

            if (!string.IsNullOrEmpty(relaciones))
            {
                query = relaciones.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, tabla) => current.Include(tabla));
            }

            query = tracking ? query.AsTracking() : query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<ICollection<TInfo>> ListarAsync<TInfo>(
            Expression<Func<TEntity, bool>> predicado, Expression<Func<TEntity, TInfo>> selector)
        {
            return await dbContext.Set<TEntity>()
                .Where(predicado)
                .AsNoTracking()
                .Select(selector)
                .ToListAsync();
        }

        public virtual async Task<(ICollection<TInfo> Coleccion, int Total)> ListarPaginadoAsync<TInfo, TKey>(
            Expression<Func<TEntity, bool>> predicado, Expression<Func<TEntity, TInfo>> selector, Expression<Func<TEntity, TKey>> orderBy,
            string relaciones, int pagina, int filas)
        {
            var query = dbContext.Set<TEntity>()
                .Where(predicado)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrEmpty(relaciones))
            {
                query = relaciones.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, tabla) => current.Include(tabla));
            }

            query = query.OrderBy(orderBy)
                .Skip((pagina - 1) * filas)
                .Take(filas)
                .AsQueryable();

            var total = await dbContext.Set<TEntity>()
                .Where(predicado)
                .CountAsync();

            return (await query
                .Select(selector)
                .ToListAsync(), total);
        }

        public async Task<(ICollection<TEntity> Coleccion, int Total)> ListarPaginadoAsync<TKey>(
            Expression<Func<TEntity, bool>> predicado, Expression<Func<TEntity, TKey>> orderBy,
            string relaciones, int pagina, int filas)
        {
            var query = dbContext.Set<TEntity>()
                .Where(predicado)
                .AsQueryable();

            if (!string.IsNullOrEmpty(relaciones))
            {
                query = relaciones.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, tabla) => current.Include(tabla));
            }

            query = query.OrderBy(orderBy)
                .Skip((pagina - 1) * filas)
                .Take(filas)
                .AsQueryable();

            var total = await dbContext.Set<TEntity>()
                .Where(predicado)
                .CountAsync();

            return (await query
                .ToListAsync(), total);
        }


        //packages: efcore.bulkextensions.sqlserver\7.1.6\
        // public virtual async Task AgregarAsync(ICollection<TEntity> entidades)
        // {
        //     dbContext.Database.SetCommandTimeout(3600);
        //     await dbContext.BulkInsertAsync(entidades);
        // }

    }
}
