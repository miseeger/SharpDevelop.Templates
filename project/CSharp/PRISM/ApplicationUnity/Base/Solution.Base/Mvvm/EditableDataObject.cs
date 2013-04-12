using System; 
using System.Collections.Generic; 
using System.Linq;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Base.Mvvm.Enum; 
using ${SolutionName}.Base.Mvvm.Interfaces;
 
namespace ${SolutionName}.Base.Mvvm
{ 

	public class EditableDataObject: DataObject, IEditableDataObject 
	{

		private readonly Dictionary<string, object> _propBackup; 
		private bool _inTxn; 


		protected EditableDataObject() 
		{ 
			_propBackup = new Dictionary<string, object>(); 
		} 


		public void BeginEdit() 
		{ 
			if (!_inTxn) 
			{ 
				var properties = GetType().GetProperties(); 
				foreach (var pi in properties.Where(pi => pi.CanWrite  
					&& !Names.PropBackupIgnoringProperties.Contains(pi.Name))) 
				{ 
					try 
					{ 
						_propBackup.Add(pi.Name, pi.GetValue(this, null)); 
					} 
					catch(ArgumentException) 
					{ 
						_propBackup[pi.Name] = pi.GetValue(this, null); 
					} 
				} 
				_inTxn = true; 
			} 
		} 


		public void EndEdit() 
		{ 
			if (_inTxn) 
			{ 
				_propBackup.Clear(); 
				_inTxn = false; 
			} 
		} 


		public void CancelEdit() 
		{ 
			if (_inTxn) 
			{ 
				var properties = GetType().GetProperties(); 

				foreach (var pi in properties.Where(pi => pi.CanWrite  
					&& !Names.PropBackupIgnoringProperties.Contains(pi.Name))) 
				{ 
					pi.SetValue(this, _propBackup[pi.Name], null); 
				} 
 
				ObjectStatus = (DataObjectStatus) _propBackup["ObjectStatus"]; 
				_inTxn = false; 
			} 

		} 

	} 

}

