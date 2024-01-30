using Microsoft.EntityFrameworkCore;
using Service.Core.Interfaces.Log;
using Service.Core.Interfaces.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Core.MySql
{
    #nullable enable
    public class BaseMySqlRepository<ModelType> : IBaseMySqlRepository<ModelType> where ModelType : BaseModel, new()
    {
        private readonly BaseDbContext _dbContext;
        private readonly ICoreLogger _logger;
        protected readonly DbSet<ModelType> _dbEntity;
        protected readonly IErrorFactory _errorList;

        public BaseMySqlRepository(BaseDbContext dbContext, ICoreLogger logger, IErrorFactory errorList)
        {
            _dbContext = dbContext;
            _logger = logger;
            _dbEntity = dbContext.Set<ModelType>();
            _errorList = errorList;
        }

        /// <summary>
        /// Return the used BaseDbContext for specific manual operation
        /// </summary>
        /// <returns>BaseDbContext as object</returns>
        public object GetDatabaseContext()
        {
            return _dbContext;
        }

        public async Task<long> Count(Expression<Func<ModelType, bool>> predicate)
        {
            try
            {
                return await _dbEntity.Where(predicate).CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorList.CreateDbException(), $"Failed to count data with predicate {predicate} because an exception has occurred.", $"predicate: {predicate}", ex);
                throw;
            }
        }

        public async Task<bool> Any(Expression<Func<ModelType, bool>> predicate)
        {
            try
            {
                return await _dbEntity.Where(predicate).AnyAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorList.CreateDbException(), $"Failed to count data with predicate {predicate} because an exception has occurred.", $"predicate: {predicate}", ex);
                throw;
            }
        }

        public async Task<ModelType> Find(params object?[]? id)
        {
            try
            {
                return await _dbEntity.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorList.CreateDbFailedToGetData(), $"Failed to get data with id {id} fields because an exception has occurred.", $"id: {id}", ex);
                throw;
            }
        }

        public async Task<ModelType> Find(Expression<Func<ModelType, bool>> predicate)
        {
            try
            {
                return await _dbEntity.Where(predicate).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorList.CreateDbException(), $"Failed to get data with predicate {predicate} because an exception has occurred.", $"predicate: {predicate}", ex);
                throw;
            }
        }

        public async Task<List<ModelType>> FindMany(Expression<Func<ModelType, bool>> predicate)
        {
            try
            {
                return await _dbEntity.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorList.CreateDbException(), $"Failed to get many data with predicate {predicate} because an exception has occurred.", $"predicate: {predicate}", ex);
                throw;
            }
        }

        public async Task<List<ModelType>> FindManyWithSortedData(int pageSize,
            int targetPage,
            bool isAscending, Expression<Func<ModelType, object>> predicate)
        {
            try
            {
                if (isAscending) return await _dbEntity.OrderBy(predicate).Skip(pageSize*targetPage).Take(pageSize).ToListAsync();
                return await _dbEntity.OrderByDescending(predicate).Skip(pageSize * targetPage).Take(pageSize).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(_errorList.CreateDbException(), $"Failed to get many data with predicate {predicate} because an exception has occurred.", $"predicate: {predicate}", ex);
                throw;
            }
        }

        /// <summary>
        /// Find Many with previous Id as anchor
        /// </summary>
        /// <typeparam name="TPreviousId">for mysql should be long type, previousId for anchor</typeparam>
        /// <param name="previousId">previousId as an anchor</param>
        /// <param name="takeSize">how many to take</param>
        /// <param name="predicate">filter</param>
        /// <returns></returns>
        public async Task<List<ModelType>> FindMany<TPreviousId>(TPreviousId previousId, int takeSize, Expression<Func<ModelType, bool>> predicate)
        {
            try
            {
                return await _dbEntity.Where(predicate).OrderBy(data => data.ID).Where(data => data.ID > (previousId as long?)).Take(takeSize).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorList.CreateDbException(), $"Failed to get many data with predicate {predicate} because an exception has occurred.", $"predicate: {predicate}", ex);
                throw;
            }
        }

        /// <summary>
        /// Find page using skip and take
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<List<ModelType>> FindPage(int skip, int take)
        {
            try
            {
                return await _dbEntity.Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorList.CreateDbException(), $"Failed to get data page with index {skip} and limit {take} because an exception has occurred.", $"index: {skip}, limit:{take}", ex);
                throw;
            }
        }
        /// <summary>
        /// Find page using filter by predicate
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<List<ModelType>> FindPage(int skip, int take, Expression<Func<ModelType, bool>> predicate)
        {
            try
            {
                return await _dbEntity.Where(predicate).Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorList.CreateDbException(), $"Failed to get data page with index {skip} and limit {take} because an exception has occurred.", $"index: {skip}, limit:{take}", ex);
                throw;
            }
        }
        /// <summary>
        /// Finda page using single sort order
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="order"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public async Task<List<ModelType>> FindPage<Tkey>(int skip, int take, Expression<Func<ModelType, Tkey>> order, bool asc = true)
        {
            try
            {
                return  asc ? await _dbEntity.OrderBy(order).Skip(skip).Take(take).ToListAsync() : await _dbEntity.OrderByDescending(order).Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorList.CreateDbException(), $"Failed to get data page with index {skip} and limit {take} because an exception has occurred.", $"index: {skip}, limit:{take}", ex);
                throw;
            }
        }
        /// <summary>
        /// Find page using multiple sort order
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="orderList"></param>
        /// <param name="sorterList"></param>
        /// <returns></returns>
        public async Task<List<ModelType>> FindPage<Tkey>(int skip, int take, List<Expression<Func<ModelType, Tkey>>> orderList, List<bool> sorterList)
        {
            try
            {
                var data = Enumerable.Empty<ModelType>().ToList();

                if (orderList.Any())
                {
                    IOrderedQueryable<ModelType> query = sorterList[0] ? _dbEntity.OrderBy(orderList[0]) : _dbEntity.OrderByDescending(orderList[0]);

                    for (int i = 0; i < orderList.Count(); i++)
                    {
                        query = sorterList[i] ? query.ThenBy(orderList[i]) : query.ThenByDescending(orderList[i]);
                    }

                    data = await query.Skip(skip).Take(take).ToListAsync(); 
                }

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorList.CreateDbException(), $"Failed to get data page with index {skip} and limit {take} because an exception has occurred.", $"index: {skip}, limit:{take}", ex);
                throw;
            }
        }
        /// <summary>
        /// Findpage using search filter and sort order
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="predicate"></param>
        /// <param name="order"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public async Task<List<ModelType>> FindPage<TKey>(int skip, int take, Expression<Func<ModelType, bool>> predicate, Expression<Func<ModelType, TKey>> order, bool asc = true)
        {
            try
            {
                return asc ? await _dbEntity.Where(predicate).OrderBy(order).Skip(skip).Take(take).ToListAsync() : await _dbEntity.Where(predicate).OrderByDescending(order).Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorList.CreateDbException(), $"Failed to get data page with index {skip} and limit {take} because an exception has occurred.", $"index: {skip}, limit:{take}", ex);
                throw;
            }
        }
        /// <summary>
        /// Find page using search filter and multiple order
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="predicate"></param>
        /// <param name="orderList"></param>
        /// <param name="sorterList"></param>
        /// <returns></returns>
        public async Task<List<ModelType>> FindPage<Tkey>(int skip, int take, Expression<Func<ModelType, bool>> predicate, List<Expression<Func<ModelType, Tkey>>> orderList, List<bool> sorterList)
        {
            try
            {
                var data = Enumerable.Empty<ModelType>().ToList();

                if (orderList.Any())
                {
                    IOrderedQueryable<ModelType> query = sorterList[0] ? _dbEntity.OrderBy(orderList[0]) : _dbEntity.OrderByDescending(orderList[0]);

                    for (int i = 0; i < orderList.Count(); i++)
                    {
                        query = sorterList[i] ? query.ThenBy(orderList[i]) : query.ThenByDescending(orderList[i]);
                    }
                        
                    data = await query.Where(predicate).Skip(skip).Take(take).ToListAsync();
                }

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorList.CreateDbException(), $"Failed to get data page with index {skip} and limit {take} because an exception has occurred.", $"index: {skip}, limit:{take}", ex);
                throw;
            }
        }

        public async Task<ModelType> Add(ModelType entity)
        {
            try
            {
                await _dbEntity.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogCritical(_errorList.CreateAuthTokenInvalid(), $"Failed to create data because an exception has occurred.", "", e);
                throw;
            }
        }
        public async Task<bool> AddRange(ICollection<ModelType> entities)
        {
            try
            {
                await _dbEntity.AddRangeAsync(entities);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (System.Exception e)
            {
                _logger.LogCritical(_errorList.CreateDbException(), $"Failed to create many data because an exception has occurred.", "", e);
                throw;
            }
        }

        public async Task<ModelType> Update(ModelType entity)
        {
            try
            {
                _dbEntity.Update(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogCritical(_errorList.CreateDbException(), $"Failed to update data because an exception has occurred.", "", e);
                throw;
            }
        }

        public async Task<bool> UpdateRange(ICollection<ModelType> entities)
        {
            try
            {
                _dbEntity.UpdateRange(entities);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (System.Exception e)
            {
                _logger.LogCritical(_errorList.CreateDbException(), $"Failed to update many data because an exception has occurred.", "", e);
                throw;
            }
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var data = await _dbEntity.FindAsync(id);
                _dbEntity.Remove(data);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogCritical(_errorList.CreateDbException(), $"Failed to delete data because an exception has occurred.", "", e);
                throw;
            }
        }

        public async Task<bool> Delete(ModelType entity)
        {
            try
            {
                _dbEntity.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogCritical(_errorList.CreateDbException(), $"Failed to delete data because an exception has occurred.", "", e);
                throw;
            }
        }

        public async Task<bool> DeleteRange(Expression<Func<ModelType, bool>> predicate)
        {
            try
            {
                var data = await _dbEntity.Where(predicate).ToListAsync();
                _dbEntity.RemoveRange(data);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogCritical(_errorList.CreateDbException(), $"Failed to delete data because an exception has occurred.", "", e);
                throw;
            }
        }

        public async Task<bool> DeleteRange(ICollection<ModelType> entities)
        {
            try
            {
                _dbContext.RemoveRange(entities);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogCritical(_errorList.CreateDbException(), $"Failed to delete many data because an exception has occurred.", "", e);
                throw;
            }
        }

        public async Task<bool> CheckConnection()
        {
            try
            {
                var isOk = await _dbContext.Database.CanConnectAsync();
                return isOk;
            }
            catch (Exception e)
            {
                _logger.LogError(_errorList.CreateDbException(), "Failed to check connection", e.Message);
                throw;
            }
        }

        private Exception NotSupportedMethodError()
        {
            var error = _errorList.CreateMySQLNotSupportedMethodError();
            _logger.LogError(error, null, "");

            return (Exception)_errorList.CreateErrorException(error);
        }

        public Task<ModelType> Get(string id)
        {
            throw NotSupportedMethodError();
        }

        public Task<ModelType> GetExclusion(string id, Expression<Func<ModelType, object>> excludeFields)
        {
            throw NotSupportedMethodError();
        }

        public Task<ModelType> GetInclusion(string id, Expression<Func<ModelType, object>> includeFields)
        {
            throw NotSupportedMethodError();
        }

        public Task<ModelType> GetInclusion(Expression<Func<ModelType, bool>> predicate, Expression<Func<ModelType, object>> includeFields)
        {
            throw NotSupportedMethodError();
        }

        public Task<ModelType> GetExclusion(Expression<Func<ModelType, bool>> predicate, Expression<Func<ModelType, object>> excludeFields)
        {
            throw NotSupportedMethodError();
        }

        public Task<IEnumerable<ModelType>> GetWithPagination(string currentId, int pageSize, int currentPage, int targetPage, bool isAscending)
        {
            throw NotSupportedMethodError();
        }

        public Task<IEnumerable<ModelType>> GetWithPagination(string currentId, int pageSize, int currentPage, int targetPage, Expression<Func<ModelType, bool>> predicate, bool isAscending)
        {
            throw NotSupportedMethodError();
        }

        public Task<ModelType> GetSingleRandom()
        {
            throw NotSupportedMethodError();
        }

        public Task<ModelType> GetSingleRandom(Expression<Func<ModelType, bool>> predicate)
        {
            throw NotSupportedMethodError();
        }

        public Task<IEnumerable<ModelType>> GetManyExclusion(Expression<Func<ModelType, bool>> predicate, Expression<Func<ModelType, object>> excludeFields)
        {
            throw NotSupportedMethodError();
        }

        public Task<bool> Delete(string id)
        {
            throw NotSupportedMethodError();
        }
    }
}
