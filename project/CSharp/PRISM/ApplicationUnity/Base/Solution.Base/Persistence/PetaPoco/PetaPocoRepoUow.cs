using System;
using System.Collections.Generic;

using ${SolutionName}.Base.Persistence.PetaPoco.DatabaseTypes;
using ${SolutionName}.Base.Persistence.PetaPoco.Internal;

namespace ${SolutionName}.Base.Persistence.PetaPoco
{

	// Repository Interface
	public interface IRepository
	{
		Database Db { get; }
		IUnitOfWork UoW { get; set; }
		T Single<T>(object primaryKey);
		IEnumerable<T> Query<T>();
		Page<T> PagedQuery<T>(long pageNumber, long itemsPerPage, string sql, params object[] args);
		Int64 Insert(object itemToAdd);
		int Update(object itemToUpdate, object primaryKeyValue);
		int Delete<T>(object primaryKeyValue);
	}
	
	// Implementation of the Repository-Interface plus Overloads
	public class Repository : IRepository, IDisposable
  	{
  		private Database _db;
  		public Database Db 
  		{ 
  			get { return _db;} 
  		}
  		

  		private IUnitOfWork _uow;
  		public IUnitOfWork UoW
  		{
  			get { return _uow;}
  			set 
  			{ 
  				_uow = value;
  				_db = _uow.Db;
  			}
  		}
  		
  		public Repository() : this("database")
  		{
  		}
  		
  		public Repository(IUnitOfWork UoWork)
  		{
  			if (UoWork == null) 
  			{
  				throw new ArgumentNullException("UnitOfWork (UoWork)");
  			}
  			UoW = UoWork as UnitOfWork;
  		}
  		
  		public Repository(string ConnectionStringName)
  		{
  			if (ConnectionStringName == null || ConnectionStringName.Trim() == "")
			{
				throw new ArgumentException("ConnectionStringName is null or empty.");
			}
			_db = new Database(ConnectionStringName);
		}
  		
  		public T Single<T>(object primaryKey)
  		{
  			return _db.Single<T>(primaryKey);
  		}
  		
  		public IEnumerable<T> Query<T>()
  		{
  			var pd = PocoData.ForType(typeof(T));
  			var sql = "SELECT * FROM " + pd.TableInfo.TableName;
		  	return _db.Query<T>(sql);
  		}
  		
  		public IEnumerable<T> Query<T>(string where = "", string orderBy = "", int limit = 0, string columns = "*", params object[] args)
  		{
  			var pd = PocoData.ForType(typeof (T));
  			string sql = BuildSql(pd.TableInfo.TableName, where, orderBy, limit, columns);
  			return Query<T>(sql, args);
  		}
  		
  		public IEnumerable<T> Query<T>(string sql, params object[] args)
  		{
  			return _db.Query<T>(sql, args);
  		}
  		
  		public Page<T> PagedQuery<T>(long pageNumber, long itemsPerPage, string sql, params object[] args)
  		{
  			return _db.Page<T>(pageNumber, itemsPerPage, sql, args);
  		}
  		
  		public Page<T> PagedQuery<T>(long pageNumber, long itemsPerPage, Sql sql)
  		{
  			return _db.Page<T>(pageNumber, itemsPerPage, sql);
  		}
  		
  		public Int64 Insert(object poco)
  		{
  			return (Int64)_db.Insert(poco);
  		}
  		
  		public Int64 Insert(string tableName, string primaryKeyName, bool autoIncrement, object poco)
  		{
  			return (Int64)_db.Insert(tableName, primaryKeyName, autoIncrement, poco);
  		}
  		
  		public Int64 Insert(string tableName, string primaryKeyName, object poco)
  		{
  			return (Int64)_db.Insert(tableName, primaryKeyName, poco);
  		}
  		
  		public int Update(object poco)
  		{
  			return _db.Update(poco);
  		}
  		
  		public int Update(object poco, object primaryKeyValue)
  		{
  			return _db.Update(poco, primaryKeyValue);
  		}
  	
  		public int Update(string tableName, string primaryKeyName, object poco)
  		{
  			return _db.Update(tableName, primaryKeyName, poco);
  		}
  		
  		public int Delete<T>(object pocoOrPrimaryKey)
  		{
  			return _db.Delete<T>(pocoOrPrimaryKey);
  		}
  		
  		public static string BuildSql(string tableName, string where = "", string orderBy = "", int limit = 0, string columns = "*")
  		{
  			string sql = limit > 0 ? "SELECT TOP " + limit + " {0} FROM {1} " : "SELECT {0} FROM {1} ";
  			if (!string.IsNullOrEmpty(where))
  				sql += where.Trim().StartsWith("where", StringComparison.CurrentCultureIgnoreCase) ? where : "WHERE " + where;
  			if (!String.IsNullOrEmpty(orderBy))
  				sql += orderBy.Trim().StartsWith("order by", StringComparison.CurrentCultureIgnoreCase) ? orderBy : " ORDER BY " + orderBy;
  			return string.Format(sql, columns, tableName);
  		}
  		
		
		public void Dispose()
		{
			if (UoW == null && _db != null) 
			{
				_db.CloseSharedConnection();
			}
		}
	}
	
	// IDisplosable UnitOfWork-Interface
	public interface IUnitOfWork : IDisposable 
	{
    	void Commit();
	    Database Db { get; }
	}
	
	// Factory-Interface for UoW
	public interface IUnitOfWorkFactory
	{
    	IUnitOfWork GetUnitOfWork();
    	IUnitOfWork GetUnitOfWork(string ConnectionStringName);
	}
	
	// Static UoW-Factory
	public class UnitOfWorkFactory : IUnitOfWorkFactory
	{
    	
		public IUnitOfWork GetUnitOfWork()
    	{
    	    return new UnitOfWork();
    	}

		public IUnitOfWork GetUnitOfWork(string ConnectionStringName)
    	{
    	    return new UnitOfWork(ConnectionStringName);
    	}

	}
	
	// Disposable IUnitOfWork Implementation
	public class UnitOfWork : IUnitOfWork
	{
    	private readonly Transaction _transaction;

    	private readonly Database _db;
	    public Database Db 
	    {
	        get { return _db; }
    	}
    	
	    public UnitOfWork() : this("database")
    	{
    	}
    	
    	public UnitOfWork(string ConnectionStringName) 
    	{
  			if (ConnectionStringName == null || ConnectionStringName.Trim() == "")
			{
				throw new ArgumentException("ConnectionStringName is null or empty.");
			}
    		_db = new Database(ConnectionStringName);
	        _transaction = new Transaction(_db);
	    }

    	public void Dispose() 
    	{
    	    _transaction.Dispose();
    	    _db.CloseSharedConnection();
	    }

    	public void Commit() 
    	{
	        _transaction.Complete();
	    }
	}
	
	
	// ------------------------------------------------------------------------
	// Usage of the PetaPoco UnitOfWork
	// ------------------------------------------------------------------------
	
	// //Creating the UoW context
	// using (var uow = UnitOfWorkFactory.GetUnitOfWork("myDatabase"))
	// {
	//     // Creating a new Repo within the UoW context:
	//     var repo = new Repository(uow);
	//	   //Do some actions:
	//	   repo.Insert(newProduct);
	//	   repo.Update(changedProduct);
	//	   repo.Delete<Product>(productToBeDeleted);
    //
	//	   //Commit work:
    //     uow.Commit();
    // }

	// ------------------------------------------------------------------------

}
