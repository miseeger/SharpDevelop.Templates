using System.Collections.Generic;

namespace ${SolutionName}.Base.Interfaces.Services
{
	
	public interface IGlobalInfoService
	{

		Dictionary<string, object> GlobalInfo {get;}
		object this[string key] {get; set;}
		
		void Clear(string key);
		void ClearAll();
		void Delete(string key);
		void DeleteAll();
		bool Exists(string key);

	}

}
