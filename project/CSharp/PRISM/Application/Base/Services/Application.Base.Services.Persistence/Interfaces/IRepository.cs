using System;
using System.Collections.Generic;

namespace ${SolutionName}.Base.Services.Persistence.Interfaces
{

	public interface IRepository : IDisposable
	{
		IDatabase Db { get; }
		IUnitOfWork UoW { get; set; }
		T Single<T>(object primaryKey);
		T SingleOrDefault<T>(object primaryKey);
		IEnumerable<T> Query<T>();
		IEnumerable<T> Query<T>(string where, string orderBy, int limit, string columns, params object[] args);
		IEnumerable<T> Query<T>(string sql, params object[] args);
		IPage<T> PagedQuery<T>(long pageNumber, long itemsPerPage, string sql, params object[] args);
		IPage<T> PagedQuery<T>(long pageNumber, long itemsPerPage, ISql sql);
		Int64 Insert(object itemToAdd);
		Int64 Insert(string tableName, string primaryKeyName, bool autoIncrement, object poco);
		Int64 Insert(string tableName, string primaryKeyName, object poco);
		int Update(object poco);
		int Update(object poco, object primaryKeyValue);
		int Update(string tableName, string primaryKeyName, object poco);
		int Delete<T>(object primaryKeyValue);
	}

}
