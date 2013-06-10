using System;

namespace ${SolutionName}.Base.Services.Persistence.Interfaces
{

	public interface IUnitOfWork : IDisposable 
	{
    	void Commit();
	    IDatabase Db { get; }
	}

	
	public interface IUnitOfWorkFactory
	{
    	IUnitOfWork GetUnitOfWork();
    	IUnitOfWork GetUnitOfWork(string connectionStringName);
	}
	
}
