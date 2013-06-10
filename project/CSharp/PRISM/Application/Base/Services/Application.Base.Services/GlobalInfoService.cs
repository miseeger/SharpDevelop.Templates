using System.Linq;
using System.Collections.Generic;
using ${SolutionName}.Base.Interfaces.Services;

namespace ${SolutionName}.Base.Services
{

	class GlobalInfoService : IGlobalInfoService
	{
		
		private Dictionary<string, object> _globalInfo;
		public Dictionary<string, object> GlobalInfo
		{
			get
			{
				return _globalInfo;
			}
		}
		

		public GlobalInfoService()
		{
			_globalInfo = new Dictionary<string, object>();
		}
		

		public object this[string key] 
		{
			get
			{
			    object result;
			    return _globalInfo.TryGetValue(key, out result) ? result : null;
			}

		    set
			{
				if (Exists(key))
				{
					_globalInfo[key] = value;
				}
				else
				{
					_globalInfo.Add(key, value);
				}

			}
		}
		

		public void Clear(string key)
		{
			_globalInfo[key] = null;
		}
		

		public void ClearAll()
		{
			var keys = _globalInfo.Keys.ToList();
			foreach (var key in keys)
			{
				_globalInfo[key] = null;
			}
		}
		
		
		public void Delete(string key)
		{
			_globalInfo.Remove(key);
		}


		public void DeleteAll()
		{
			_globalInfo.Clear();
		}

		
		public bool Exists(string key)
		{
			return _globalInfo.Any(e => (e.Key == key));
		}
		
	}
	
}
