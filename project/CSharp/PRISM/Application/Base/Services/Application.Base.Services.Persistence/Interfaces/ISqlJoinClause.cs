namespace ${SolutionName}.Base.Services.Persistence.Interfaces
{

	public interface ISqlJoinClause
	{
		ISql On(string onClause, params object[] args);
	}
	
}
