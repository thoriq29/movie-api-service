using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Core.Interfaces.Repository
{
    public interface IBaseRepository<T>
    {

        //// Method used in MySQL and NoSQL

        /// <summary>
        /// Return the used DatabaseContext for specific operation
        /// </summary>
        /// <returns>BaseDbContext (MySql) or MongoCollection<DocumentModel> as an object</returns>
        public object GetDatabaseContext();

        /// <summary>
        /// Get the data with predicate. (note: keep it mind when using predicate the result possible >1 data, so the returned data will be the first data)
        /// </summary>
        /// <param name="predicate">Defined predicate for getting data.</param>
        /// <returns>Defined predicate for getting data.</returns>
        public Task<T> Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get many data with predicate in one call.
        /// </summary>
        /// <param name="predicate">Defined predicate for getting data.</param>
        /// <returns>Return collection of data.</returns>
        public Task<List<T>> FindMany(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get many data with predicate using previousId ant take size.
        /// </summary>
        /// <param name="previousId">previousId as an anchor (won't be taken for the query) </param>
        /// <param name="takeSize">how many should be taken from previousId</param>
        /// <param name="predicate">Defined predicate for getting data.</param>
        /// <returns>Return collection of data.</returns>
        public Task<List<T>> FindMany<TPreviousId>(TPreviousId previousId, int takeSize, Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Create data based on input model.
        /// </summary>
        /// <param name="model">Model of the data to be created.</param>
        /// <returns>Return boolean of the result state.</returns>
        public Task<T> Add(T entity);

        /// <summary>
        /// Delete many data with predicate in one call.
        /// </summary>
        /// <param name="predicate">Defined predicate for deleting data.</param>
        /// <returns>Return boolean of the result state.</returns>
        public Task<bool> DeleteRange(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Update document with defined model.
        /// </summary>
        /// <param name="model">Model to be updated</param>
        /// <returns>Return boolean of the result state.</returns>
        public Task<T> Update(T model);

        /// <summary>
        /// Create many data based on input models in one call.
        /// </summary>
        /// <param name="models">Model collection of the data to be created.</param>
        /// <returns>Return boolean of the result state.</returns>
        public Task<bool> AddRange(ICollection<T> entities);

        /// <summary>
        /// Count documents.
        /// </summary>
        /// <param name="predicate">Defined predicate for counting documents.</param>
        /// <returns>Return the document count.</returns>
        public Task<long> Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Checking connection to database driver.
        /// </summary>
        /// <returns>Return boolean of the result state.</returns>
        public Task<bool> CheckConnection();


        //// Method only used in NoSQL

        /// <summary>
        /// Get the document with defined ID.
        /// </summary>
        /// <param name="id">ID of the document.</param>
        /// <returns>Return get model result.</returns>
        Task<T> Get(string id);

        /// <summary>
        /// Get the document with defined ID.
        /// </summary>
        /// <param name="id">ID of the document.</param>
        /// <param name="excludeFields">Exclude the selected fields.</param>
        /// <returns>Return get model result.</returns>
        Task<T> GetExclusion(string id, Expression<Func<T, object>> excludeFields);

        /// <summary>
        /// Get the document with defined ID.
        /// </summary>
        /// <param name="id">ID of the document.</param>
        /// <param name="includeFields">include only the selected fields.</param>
        /// <returns>Return get model result.</returns>
        Task<T> GetInclusion(string id, Expression<Func<T, object>> includeFields);

        /// <summary>
        /// Get the document with defined ID.
        /// </summary>
        /// <param name="predicate">Defined predicate for getting documents.</param>
        /// <param name="includeFields">include only the selected fields.</param>
        /// <returns>Return get model result.</returns>
        Task<T> GetInclusion(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> includeFields);

        /// <summary>
        /// Get the document with predicate. (note: keep it mind when using predicate the result possible >1 document, so the returned document will be the first document)
        /// </summary>
        /// <param name="predicate">Defined predicate for getting documents.</param>
        /// <param name="excludeFields">Exclude the selected fields.</param>
        /// <returns>Defined predicate for getting documents.</returns>
        Task<T> GetExclusion(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> excludeFields);

        /// <summary>
        /// Get many documents with ID-based pagination in one call.
        /// </summary>
        /// <param name="currentId">The current ObjectId (first or top (in FE) of the list)</param>
        /// <param name="pageSize">How much rows to display</param>
        /// <param name="currentPage">The current page number when sending request</param>
        /// <param name="targetPage">The target page number when clicking the desired page number</param>
        /// <param name="isAscending">The data will be sorted ascending by ID</param>
        /// <returns>Return collection of documents.</returns>
        public Task<IEnumerable<T>> GetWithPagination(string? currentId, int pageSize, int currentPage, int targetPage, bool isAscending);

        /// <summary>
        /// Get many documents with ID-based pagination in one call.
        /// </summary>
        /// <param name="currentId">The current ObjectId (first or top (in FE) of the list)</param>
        /// <param name="pageSize">How much rows to display</param>
        /// <param name="currentPage">The current page number when sending request</param>
        /// <param name="targetPage">The target page number when clicking the desired page number</param>
        /// <param name="predicate">Defined predicate for getting documents.</param>
        /// <param name="isAscending">The data will be sorted ascending by ID</param>
        /// <returns>Return collection of documents.</returns>
        public Task<IEnumerable<T>> GetWithPagination(string? currentId, int pageSize, int currentPage, int targetPage, Expression<Func<T, bool>> predicate, bool isAscending);

        /// <summary>
        /// Get single random document
        /// </summary>
        /// <returns>Return random model result.</returns>
        Task<T> GetSingleRandom();

        /// <summary>
        /// Get single random document with predicate
        /// </summary>
        /// <returns>Return random model result.</returns>
        Task<T> GetSingleRandom(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get many documents with predicate in one call.
        /// </summary>
        /// <param name="predicate">Defined predicate for getting documents.</param>
        /// <param name="excludeFields">Exclude the selected fields.</param>
        /// <returns>Return collection of documents.</returns>
        Task<IEnumerable<T>> GetManyExclusion(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> excludeFields);


        //// Method only used in MySQL

        /// <summary>
        /// Check data based on predicate.
        /// </summary>
        /// <param name="predicate">Defined predicate for counting data.</param>
        /// <returns>Return the data count.</returns>
        public Task<bool> Any(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get the data with defined ID.
        /// </summary>
        /// <param name="id">ID of the data.</param>
        /// <returns>Return get model result.</returns>
        public Task<T> Find(params object?[]? id);

        /// <summary>
        /// Get many data with index and limit
        /// </summary>
        /// <param name="skip">Index of data.</param>
        /// <param name="take">Limit data taken.</param>
        /// <returns>Return collection of data.</returns>
        public Task<List<T>> FindPage(int skip, int take);

        /// <summary>
        /// Get many data with index, limit and predicate
        /// </summary>
        /// <param name="skip">Index of data.</param>
        /// <param name="take">Limit data taken.</param>
        /// <param name="predicate">Defined predicate to get data.</param>
        /// <returns>Return collection of data.</returns>
        public Task<List<T>> FindPage(int skip, int take, Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get many data with index, limit and predicate
        /// </summary>
        /// <param name="skip">Index of data.</param>
        /// <param name="take">Limit data taken.</param>
        /// <param name="order">Order data based on defined order</param>
        /// <returns>Return collection of data.</returns>
        public Task<List<T>> FindPage<TKey>(int skip, int take, Expression<Func<T, TKey>> order, bool asc = true);

        /// <summary>
        /// Get many data with index, limit and predicate
        /// </summary>
        /// <param name="skip">Index of data.</param>
        /// <param name="take">Limit data taken.</param>
        /// <param name="order">Order data based on defined order</param>
        /// <returns>Return collection of data.</returns>
        public Task<List<T>> FindPage<Tkey>(int skip, int take, List<Expression<Func<T, Tkey>>> orderList, List<bool> sorterList);

        /// <summary>
        /// Get many data with index, limit and predicate
        /// </summary>
        /// <param name="skip">Index of data.</param>
        /// <param name="take">Limit data taken.</param>
        /// <param name="order">Order data based on defined order</param>
        /// <param name="predicate">Defined predicate to get data</param>
        /// <returns>Return collection of data.</returns>
        public Task<List<T>> FindPage<TKey>(int skip, int take, Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> order, bool asc = true);

        /// <summary>
        /// Get many documents with sorted data pagination in one call.
        /// </summary>
        /// <param name="pageSize">How much rows to display</param>
        /// <param name="targetPage">The target page number when clicking the desired page number</param>
        /// <param name="isAscending">The data will be sorted ascending or descending</param>
        /// <param name="predicate">The data that will be sorted</param>
        /// <returns>Return collection of documents.</returns>
        Task<List<T>> FindManyWithSortedData(
            int pageSize,
            int targetPage,
            bool isAscending,
            Expression<Func<T, object>> predicate);

        /// <summary>
        /// Get many data with index, limit and predicate
        /// </summary>
        /// <param name="skip">Index of data.</param>
        /// <param name="take">Limit data taken.</param>
        /// <param name="order">Order data based on defined order</param>
        /// <param name="predicate">Defined predicate to get data</param>
        /// <returns>Return collection of data.</returns>

        public Task<List<T>> FindPage<Tkey>(int skip, int take, Expression<Func<T, bool>> predicate, List<Expression<Func<T, Tkey>>> orderList, List<bool> sorterList);

        /// <summary>
        /// Update many data with collection of defined model in one call.
        /// </summary>
        /// <param name="model">Model to be updated</param>
        /// <returns>Return boolean of the result state.</returns>
        public Task<bool> UpdateRange(ICollection<T> entities);

        /// <summary>
        /// Delete document with defined ID.
        /// </summary>
        /// <param name="id">ID of the document.</param>
        /// <returns>Return boolean of the result state.</returns>
        public Task<bool> Delete(long id);

        /// <summary>
        /// Delete document with defined ID.
        /// </summary>
        /// <param name="id">ID of the document.</param>
        /// <returns>Return boolean of the result state.</returns>
        public Task<bool> Delete(string id);

        /// <summary>
        /// Delete data with defined model.
        /// </summary>
        /// <param name="id">ID of the document.</param>
        /// <returns>Return boolean of the result state.</returns>
        public Task<bool> Delete(T entity);

        /// <summary>
        /// Delete many data with collection of defined model in one call.
        /// </summary>
        /// <param name="predicate">Defined predicate for deleting data.</param>
        /// <returns>Return boolean of the result state.</returns>
        public Task<bool> DeleteRange(ICollection<T> entities);
    }
}
