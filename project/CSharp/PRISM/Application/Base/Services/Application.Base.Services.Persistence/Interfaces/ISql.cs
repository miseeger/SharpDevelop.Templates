using System.Collections.Generic;
using System.Text;

namespace ${SolutionName}.Base.Services.Persistence.Interfaces
{

	public interface ISql
	{
		ISql Builder { get; } // supposed to be implemented as static
		void Build(); 
		string SQL { get; }
        string SqlRo { get; }
		object[] Arguments { get; }
		ISql Append(ISql sql);
		ISql Append(string sql, params object[] args);
		bool Is(ISql sql, string sqltype);
		void Build(StringBuilder sb, List<object> args, ISql lhs);
		ISql Where(string sql, params object[] args);
		ISql OrderBy(params object[] columns);
		ISql Select(params object[] columns);
		ISql From(params object[] tables);
		ISql GroupBy(params object[] columns);
		ISqlJoinClause Join(string JoinType, string table);
		ISqlJoinClause InnerJoin(string table);
		ISqlJoinClause LeftJoin(string table);
	}

}
