// using MethodTimer;
using Service.Core.Interfaces.Log;
using Service.Core.Interfaces.Repository;
using Service.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Core.Framework.Services
{
    public class BaseService<T> : IBaseService<T> where T : class, new()
    {
        protected readonly IBaseRepository<T> _baseRepository;
        protected readonly ICoreLogger _logger;
        protected readonly IErrorFactory _errorFactory;
        public BaseService(IBaseRepository<T> baseRepository, ICoreLogger logger, IErrorFactory errorFactory)
        {
            _baseRepository = baseRepository;
            _logger = logger;
            _errorFactory = errorFactory;
        }

        public async Task<T> Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _baseRepository.Find(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbException(), $"Failed to get data with predicate {predicate} because an exception has occurred.", $"predicate: {predicate}", ex);
                throw;
            }
        }
        public async Task<List<T>> FindMany(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _baseRepository.FindMany(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbException(), $"Failed to get many data with predicate {predicate} and given excluded fields because an exception has occurred.", $"predicate: {predicate}", ex);
                throw;
            }
        }
        virtual public async Task<T> Add(T entity)
        {
            try
            {
                return await _baseRepository.Add(entity);
            }
            catch (Exception e)
            {
                _logger.LogCritical(_errorFactory.CreateAuthTokenInvalid(), $"Failed to create data because an exception has occurred.", "", e);
                throw;
            }
        }
        public async Task<bool> DeleteRange(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _baseRepository.DeleteRange(predicate);
            }
            catch (Exception e)
            {
                _logger.LogCritical(_errorFactory.CreateDbException(), $"Failed to delete data because an exception has occurred.", "", e);
                throw;
            }
        }
        // [Time("RepositoryUpdate")]
        public async Task<T> Update(T entity)
        {
            try
            {
                return await _baseRepository.Update(entity);
            }
            catch (Exception e)
            {
                _logger.LogCritical(_errorFactory.CreateDbException(), $"Failed to update data because an exception has occurred.", "", e);
                throw;
            }
        }
        public async Task<bool> AddRange(ICollection<T> entities)
        {
            try
            {
                return await _baseRepository.AddRange(entities);
            }
            catch (System.Exception e)
            {
                _logger.LogCritical(_errorFactory.CreateDbException(), $"Failed to create many data because an exception has occurred.", "", e);
                throw;
            }
        }
        public async Task<long> Count(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _baseRepository.Count(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbException(), $"Failed to count data with predicate {predicate} because an exception has occurred.", $"predicate: {predicate}", ex);
                throw;
            }
        }
        public async Task<bool> CheckConnection()
        {
            try
            {
                return await _baseRepository.CheckConnection();
            }
            catch (Exception e)
            {
                _logger.LogCritical(_errorFactory.CreateDbException(), $"Failed to check connection because an exception has occurred.", "", e);
                throw;
            }
        }
        public async Task<T> Find(string id)
        {
            try
            {
                return await _baseRepository.Get(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbFailedToGetData(), $"Failed to get data with ID {id} because an exception has occurred.", $"id: {id}", ex);
                throw ex;
            }
        }
        public async Task<T> FindExclusion(string id, Expression<Func<T, object>> excludeFields)
        {
            try
            {
                return await _baseRepository.GetExclusion(id, excludeFields);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbFailedToGetData(), $"Failed to get data with ID {id} and given excluded fields because an exception has occurred.", $"id: {id}, excludeFields: {excludeFields}", ex);
                throw ex;
            }
        }
        public async Task<T> FindInclusion(string id, Expression<Func<T, object>> includeFields)
        {
            try
            {
                return await _baseRepository.GetInclusion(id, includeFields);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbFailedToGetData(), $"Failed to get data with ID {id} and given included fields because an exception has occurred.", $"id: {id}, includeFields: {includeFields}", ex);
                throw ex;
            }
        }
        public async Task<T> FindInclusion(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> includeFields)
        {
            try
            {
                return await _baseRepository.GetInclusion(predicate, includeFields);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbFailedToGetData(), "Failed to get data with given predicate and given included fields because an exception has occurred.", $"predicate: {predicate}, includeFields: {includeFields}", ex);
                throw ex;
            }
        }
        public async Task<T> FindExclusion(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> excludeFields)
        {
            try
            {
                return await _baseRepository.GetExclusion(predicate, excludeFields);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbFailedToGetData(), "Failed to get data with given predicate and excluded fields because an exception has occurred.", $"predicate: {predicate}, excludeFields: {excludeFields}", ex);
                throw ex;
            }
        }
        public async Task<IEnumerable<T>> SelectWithPagination(string currentId, int pageSize, int currentPage, int targetPage, bool isAscending)
        {
            try
            {
                return await _baseRepository.GetWithPagination(currentId, pageSize, currentPage, targetPage, isAscending);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbFailedToGetData(), "Failed to get paginated data because an exception has occurred.", $@"currentId: {currentId}, pageSize: {pageSize}, currentPage: {currentPage}, targetPage: {targetPage}, isAscending: {isAscending}", ex);
                throw ex;
            }
        }
        public async Task<IEnumerable<T>> SelectWithPagination(string currentId, int pageSize, int currentPage, int targetPage, Expression<Func<T, bool>> predicate, bool isAscending)
        {
            try
            {
                return await _baseRepository.GetWithPagination(currentId, pageSize, currentPage, targetPage, predicate, isAscending);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbFailedToGetData(), "Failed to get paginated data because an exception has occurred.", $@"currentId: {currentId}, pageSize: {pageSize}, currentPage: {currentPage}, targetPage: {targetPage}, isAscending: {isAscending}", ex);
                throw ex;
            }
        }
        public async Task<T> FindSingleRandom()
        {
            try
            {
                return await _baseRepository.GetSingleRandom();
            }
            catch (Exception ex)
            {
                //TODO: insert the proper exception flow
                _logger.LogError(_errorFactory.CreateDbFailedToGetData(), "Failed to get random data because an exception has occurred.", "", ex);
                throw ex;
            }
        }

        public async Task<T> FindSingleRandom(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _baseRepository.GetSingleRandom(predicate);
            }
            catch (Exception ex)
            {
                //TODO: insert the proper exception flow
                _logger.LogError(_errorFactory.CreateDbFailedToGetData(), "Failed to get data with given predicate because an exception has occurred.", $"predicate: {predicate}", ex);
                throw ex;
            }
        }
        public async Task<IEnumerable<T>> SelectManyExclusion(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> excludeFields)
        {
            try
            {
                return await _baseRepository.GetManyExclusion(predicate, excludeFields);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbFailedToGetData(), "Failed to get data list filtered by predicate and excluded fields because an exception has occurred.", $"predicate: {predicate}, excludeFields: {excludeFields}", ex);
                throw ex;
            }
        }
        public async Task<bool> Any(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _baseRepository.Any(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbException(), $"Failed to count data with predicate {predicate} because an exception has occurred.", $"predicate: {predicate}", ex);
                throw;
            }
        }
#nullable enable
        public async Task<T> Find(params object?[]? id)
        {
            try
            {
                return await _baseRepository.Find(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbFailedToGetData(), $"Failed to get data with ID {id} and given excluded fields because an exception has occurred.", $"id: {id}", ex);
                throw;
            }
        }
        public async Task<List<T>> FindPage(int skip, int take)
        {
            try
            {
                return await _baseRepository.FindPage((skip - 1) * take, take);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbException(), $"Failed to get data page with index {skip} and limit {take} because an exception has occurred.", $"index: {skip}, limit:{take}", ex);
                throw;
            }
        }
        public async Task<List<T>> FindPage(int skip, int take, Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _baseRepository.FindPage((skip - 1) * take, take, predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbException(), $"Failed to get data page with index {skip} and limit {take} because an exception has occurred.", $"index: {skip}, limit:{take}", ex);
                throw;
            }
        }
        public async Task<List<T>> FindPage<TKey>(int skip, int take, Expression<Func<T, TKey>> order, bool asc = true)
        {
            try
            {
                return await _baseRepository.FindPage((skip - 1) * take, take, order, asc);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbException(), $"Failed to get data page with index {skip} and limit {take} because an exception has occurred.", $"index: {skip}, limit:{take}", ex);
                throw;
            }
        }
        public async Task<List<T>> FindPage<Tkey>(int skip, int take, List<Expression<Func<T, Tkey>>> orderList, List<bool> sorterList)
        {
            try
            {
                return await _baseRepository.FindPage((skip - 1) * take, take, orderList, sorterList);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbException(), $"Failed to get data page with index {skip} and limit {take} because an exception has occurred.", $"index: {skip}, limit:{take}", ex);
                throw;
            }
        }
        public async Task<List<T>> FindPage<Tkey>(int skip, int take, Expression<Func<T, bool>> predicate, List<Expression<Func<T, Tkey>>> orderList, List<bool> sorterList)
        {
            try
            {
                return await _baseRepository.FindPage((skip - 1) * take, take, predicate, orderList, sorterList);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbException(), $"Failed to get data page with index {skip} and limit {take} because an exception has occurred.", $"index: {skip}, limit:{take}", ex);
                throw;
            }
        }
        public async Task<List<T>> FindPage<TKey>(int skip, int take, Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> order, bool asc = true)
        {
            try
            {
                return await _baseRepository.FindPage((skip - 1) * take, take, predicate, order, asc);
            }
            catch (Exception ex)
            {
                _logger.LogError(_errorFactory.CreateDbException(), $"Failed to get data page with index {skip} and limit {take} because an exception has occurred.", $"index: {skip}, limit:{take}", ex);
                throw;
            }
        }
        // [Time("RepositoryUpdateRange")]

        public async Task<bool> UpdateRange(ICollection<T> entities)
        {
            try
            {
                return await _baseRepository.UpdateRange(entities);
            }
            catch (System.Exception e)
            {
                _logger.LogCritical(_errorFactory.CreateDbException(), $"Failed to update many data because an exception has occurred.", "", e);
                throw;
            }
        }
        virtual public async Task<bool> Delete(long id)
        {
            try
            {
                return await _baseRepository.Delete(id);
            }
            catch (Exception e)
            {
                _logger.LogCritical(_errorFactory.CreateDbException(), $"Failed to delete data because an exception has occurred.", "", e);
                throw;
            }
        }
        public async Task<bool> Delete(string id)
        {
            try
            {
                return await _baseRepository.Delete(id);
            }
            catch (Exception e)
            {
                _logger.LogCritical(_errorFactory.CreateDbException(), $"Failed to delete data because an exception has occurred.", "", e);
                throw;
            }
        }
        public async Task<bool> Delete(T entity)
        {
            try
            {
                return await _baseRepository.Delete(entity);
            }
            catch (Exception e)
            {
                _logger.LogCritical(_errorFactory.CreateDbException(), $"Failed to delete data because an exception has occurred.", "", e);
                throw;
            }
        }
        public async Task<bool> DeleteRange(ICollection<T> entities)
        {
            try
            {
                return await _baseRepository.DeleteRange(entities);
            }
            catch (Exception e)
            {
                _logger.LogCritical(_errorFactory.CreateDbException(), $"Failed to delete many data because an exception has occurred.", "", e);
                throw;
            }
        }
    }
}
