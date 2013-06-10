using System;
using System.Collections.Generic;
using System.Data;

namespace ${SolutionName}.Base.Services.Persistence.Interfaces
{

	public interface IDatabase : IDisposable
	{

		// Connection Management
		bool KeepConnectionAlive { get; set; }
		void OpenSharedConnection();
		void CloseSharedConnection();
		IDbConnection Connection { get; }

		// Transaction Management
		ITransaction GetTransaction();
		void OnBeginTransaction(); // supposed to be implemented as virtual
		void OnEndTransaction(); // supposed to be implemented as virtual
		void BeginTransaction();
		void CleanupTransaction();
		void AbortTransaction();
		void CompleteTransaction();

		// Command Management
		IDbCommand CreateCommand(IDbConnection connection, string sql, params object[] args);
		bool OnException(Exception x); // supposed to be implemented as virtual
		IDbConnection OnConnectionOpened(IDbConnection conn); // supposed to be implmented as virtual
		void OnConnectionClosing(IDbConnection conn); // supposed to be implemented as virtual
		void OnExecutingCommand(IDbCommand cmd); // supposed to be implemented as virtual
		void OnExecutedCommand(IDbCommand cmd); // supposed to be implemented as virtual

		// Operation: Execute 
		int Execute(string sql, params object[] args);
		int Execute(ISql sql);

		// Operation: ExecuteScalar
		T ExecuteScalar<T>(string sql, params object[] args);
		T ExecuteScalar<T>(ISql sql);

		// Operation: Fetch
		List<T> Fetch<T>(string sql, params object[] args);
		List<T> Fetch<T>(ISql sql);

		// Operation: Page
		IPage<T> Page<T>(long page, long itemsPerPage, string sqlCount, object[] countArgs, string sqlPage, object[] pageArgs);
		IPage<T> Page<T>(long page, long itemsPerPage, string sql, params object[] args);
		IPage<T> Page<T>(long page, long itemsPerPage, ISql sql);
		IPage<T> Page<T>(long page, long itemsPerPage, ISql sqlCount, ISql sqlPage);

		// Operation: Fetch (page)
		List<T> Fetch<T>(long page, long itemsPerPage, string sql, params object[] args);
		List<T> Fetch<T>(long page, long itemsPerPage, ISql sql);

		// Operation: SkipTake
		List<T> SkipTake<T>(long skip, long take, string sql, params object[] args);
		List<T> SkipTake<T>(long skip, long take, ISql sql);

		// Operation: Query
		IEnumerable<T> Query<T>(string sql, params object[] args);
		IEnumerable<T> Query<T>(ISql sql);

		// operation: Exists
		bool Exists<T>(string sqlCondition, params object[] args);
		bool Exists<T>(object primaryKey);

		// Operation: linq style (Exists, Single, SingleOrDefault etc...)
		T Single<T>(object primaryKey);
		T SingleOrDefault<T>(object primaryKey);
		T Single<T>(string sql, params object[] args);
		T SingleOrDefault<T>(string sql, params object[] args);
		T First<T>(string sql, params object[] args); 
		T FirstOrDefault<T>(string sql, params object[] args); 
		T Single<T>(ISql sql); 
		T SingleOrDefault<T>(ISql sql); 
		T First<T>(ISql sql); 
		T FirstOrDefault<T>(ISql sql); 

		// Operation: Insert
		object Insert(string tableName, string primaryKeyName, object poco);
		object Insert(string tableName, string primaryKeyName, bool autoIncrement, object poco);
		object Insert(object poco);

		// Operation: Update
		int Update(string tableName, string primaryKeyName, object poco, object primaryKeyValue);
		int Update(string tableName, string primaryKeyName, object poco, object primaryKeyValue, IEnumerable<string> columns);
		int Update(string tableName, string primaryKeyName, object poco);
		int Update(string tableName, string primaryKeyName, object poco, IEnumerable<string> columns);
		int Update(object poco, IEnumerable<string> columns);
		int Update(object poco);
		int Update(object poco, object primaryKeyValue);
		int Update(object poco, object primaryKeyValue, IEnumerable<string> columns);
		int Update<T>(string sql, params object[] args);
		int Update<T>(ISql sql);

		// Operation: Delete
		int Delete(string tableName, string primaryKeyName, object poco);
		int Delete(string tableName, string primaryKeyName, object poco, object primaryKeyValue);
		int Delete(object poco);
		int Delete<T>(object pocoOrPrimaryKey);
		int Delete<T>(string sql, params object[] args);
		int Delete<T>(ISql sql);

		// Operation: IsNew
		bool IsNew(string primaryKeyName, object poco);
		bool IsNew(object poco);

		// Operation: Save
		void Save(string tableName, string primaryKeyName, object poco);
		void Save(object poco);

		// Operation: Multi-Poco Query/Fetch
		List<TRet> Fetch<T1, T2, TRet>(Func<T1, T2, TRet> cb, string sql, params object[] args);
		List<TRet> Fetch<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> cb, string sql, params object[] args); 
		List<TRet> Fetch<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> cb, string sql, params object[] args); 
		IEnumerable<TRet> Query<T1, T2, TRet>(Func<T1, T2, TRet> cb, string sql, params object[] args); 
		IEnumerable<TRet> Query<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> cb, string sql, params object[] args);
		IEnumerable<TRet> Query<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> cb, string sql, params object[] args); 
		List<TRet> Fetch<T1, T2, TRet>(Func<T1, T2, TRet> cb, ISql sql); 
		List<TRet> Fetch<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> cb, ISql sql); 
		List<TRet> Fetch<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> cb, ISql sql); 
		IEnumerable<TRet> Query<T1, T2, TRet>(Func<T1, T2, TRet> cb, ISql sql); 
		IEnumerable<TRet> Query<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> cb, ISql sql); 
		IEnumerable<TRet> Query<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> cb, ISql sql); 
		List<T1> Fetch<T1, T2>(string sql, params object[] args); 
		List<T1> Fetch<T1, T2, T3>(string sql, params object[] args); 
		List<T1> Fetch<T1, T2, T3, T4>(string sql, params object[] args); 
		IEnumerable<T1> Query<T1, T2>(string sql, params object[] args); 
		IEnumerable<T1> Query<T1, T2, T3>(string sql, params object[] args); 
		IEnumerable<T1> Query<T1, T2, T3, T4>(string sql, params object[] args); 
		List<T1> Fetch<T1, T2>(ISql sql); 
		List<T1> Fetch<T1, T2, T3>(ISql sql); 
		List<T1> Fetch<T1, T2, T3, T4>(ISql sql); 
		IEnumerable<T1> Query<T1, T2>(ISql sql); 
		IEnumerable<T1> Query<T1, T2, T3>(ISql sql); 
		IEnumerable<T1> Query<T1, T2, T3, T4>(ISql sql); 
		IEnumerable<TRet> Query<TRet>(Type[] types, object cb, string sql, params object[] args);

		// Last Command
		string LastSQL { get; }
		object[] LastArgs { get;  }
		string LastCommand { get;  }

		// FormatCommand
		string FormatCommand(IDbCommand cmd);
		string FormatCommand(string sql, object[] args);

		// Public Properties
		bool EnableAutoSelect { get; set; }
		bool EnableNamedParams { get; set; }
		int CommandTimeout { get; set; }
		int OneTimeCommandTimeout { get; set; }

	}

}
