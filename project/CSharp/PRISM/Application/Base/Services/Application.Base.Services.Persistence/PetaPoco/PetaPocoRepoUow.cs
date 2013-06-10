using System;
using System.Collections.Generic;
using System.Linq;
using ${SolutionName}.Base.Services.Persistence.Interfaces;
using ${SolutionName}.Base.Services.Persistence.PetaPoco.Internal;

namespace ${SolutionName}.Base.Services.Persistence.PetaPoco
{

	// Implementation of the Repository-Interface plus Overloads
	public class Repository : IRepository
  	{
  		private IDatabase _db;
  		public IDatabase Db 
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
  		
  		public Repository(IUnitOfWork uoWork)
  		{
  			if (uoWork == null) 
  			{
  				throw new ArgumentNullException("UnitOfWork (UoWork)");
  			}
  			UoW = uoWork as UnitOfWork;
  		}
  		
  		public Repository(string connectionStringName)
  		{
  			if (connectionStringName == null || connectionStringName.Trim() == "")
			{
				throw new ArgumentException("ConnectionStringName is null or empty.");
			}
			_db = new Database(connectionStringName);
		}
  		
  		public T Single<T>(object primaryKey)
  		{
  			var result = _db.Single<T>(primaryKey);

  			if (result != null)
  			{
  				result.ResetStatusIfDataObject();
  			}
  			
  			return result;
  		}
  		
  		public T SingleOrDefault<T>(object primaryKey)
  		{
  			var result = _db.SingleOrDefault<T>(primaryKey);
  			
  			if (result != null)
  			{
  				result.ResetStatusIfDataObject();
  			}
  			
  			return result;  			
  		}
  		
  		public IEnumerable<T> Query<T>()
  		{
  			var pd = PocoData.ForType(typeof(T));
  			var sql = "SELECT * FROM " + pd.TableInfo.TableName;
  			var result = _db.Query<T>(sql);

  			if (result != null)
  			{
  				result = result.Select(p => p.ResetStatusIfDataObject()).ToList();
  			}
  			
  			return result;  			
  		}
  		
  		public IEnumerable<T> Query<T>(string where = "", string orderBy = "", int limit = 0, string columns = "*", params object[] args)
  		{
  			var pd = PocoData.ForType(typeof (T));
  			var sql = BuildSql(pd.TableInfo.TableName, where, orderBy, limit, columns);
  			return Query<T>(sql, args);
  		}
  		
  		public IEnumerable<T> Query<T>(string sql, params object[] args)
  		{
  			var result = _db.Query<T>(sql, args);
  			
  			if (result != null)
  			{
  				result = result.Select(p => p.ResetStatusIfDataObject()).ToList();
  			}
  			
  			return result;
  		}
  		
  		
  		public IPage<T> PagedQuery<T>(long pageNumber, long itemsPerPage, string sql, params object[] args)
  		{
  			var result = _db.Page<T>(pageNumber, itemsPerPage, sql, args);
  			
  			if (result != null)
  			{
  				result.Items = result.Items.Select(p => p.ResetStatusIfDataObject()).ToList();
  			}
  			
  			return result;
  		}

	    public IPage<T> PagedQuery<T>(long pageNumber, long itemsPerPage, ISql sql)
  		{
  			var result = _db.Page<T>(pageNumber, itemsPerPage, sql);
  			
  			if (result != null)
 			{
  				result.Items = result.Items.Select(p => p.ResetStatusIfDataObject()).ToList();
  			}
  			
  			return result;
  		}
		
  		public Int64 Insert(object poco)
  		{
  			var result = (Int64)_db.Insert(poco);
  			poco.ResetStatusIfDataObject();
  			return result;
  		}
  		
  		public Int64 Insert(string tableName, string primaryKeyName, bool autoIncrement, object poco)
  		{
  			var result = (Int64)_db.Insert(tableName, primaryKeyName, autoIncrement, poco);
  			poco.ResetStatusIfDataObject();
  			return result;
  		}
  		
  		public Int64 Insert(string tableName, string primaryKeyName, object poco)
  		{
  			var result = (Int64)_db.Insert(tableName, primaryKeyName, poco);
  			poco.ResetStatusIfDataObject();
  			return result;
  		}
  		
  		public int Update(object poco)
  		{
  			var result = _db.Update(poco);
  			poco.ResetStatusIfDataObject();
  			return result;
  		}
  		
  		public int Update(object poco, object primaryKeyValue)
  		{
  			var result = _db.Update(poco, primaryKeyValue);
  			poco.ResetStatusIfDataObject();
  			return result;
  		}
  	
  		public int Update(string tableName, string primaryKeyName, object poco)
  		{
  			var result = _db.Update(tableName, primaryKeyName, poco);
  			poco.ResetStatusIfDataObject();
  			return result;
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
	
	
	// Static UoW-Factory
	public class UnitOfWorkFactory : IUnitOfWorkFactory
	{
    	
		public IUnitOfWork GetUnitOfWork()
    	{
    	    return new UnitOfWork();
    	}

		public IUnitOfWork GetUnitOfWork(string connectionStringName)
    	{
    	    return new UnitOfWork(connectionStringName);
    	}

	}
	
	// Disposable IUnitOfWork Implementation
	public class UnitOfWork : IUnitOfWork
	{
    	private readonly Transaction _transaction;

    	private readonly IDatabase _db;
	    public IDatabase Db 
	    {
	        get { return _db; }
    	}
    	
	    public UnitOfWork() : this("database")
    	{
    	}
    	
    	public UnitOfWork(string connectionStringName) 
    	{
  			if (connectionStringName == null || connectionStringName.Trim() == "")
			{
				throw new ArgumentException("ConnectionStringName is null or empty.");
			}
    		_db = new Database(connectionStringName);
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
