using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using ${SolutionName}.Base.Enums;
using ${SolutionName}.Base.Mvvm;

namespace ${SolutionName}.Base.Navigation
{

	public class CommandItem : NotifyableObject
	{

		public string Caption { get; set; }
		public CommandType Type { get; set; } 
		public ImageSource Icon { get; set; }
		public string IconSize { get; set; }
		public string CommandUiType { get; set; }
		public ICommand Command { get; set; }
		public object CommandParameter { get; set; }
		public string SizeDefinition { get; set; }
		public string GroupName { get; set; }
		public string RadioGroupName { get; set; }
		public int Order { get; set; }
		
		private bool _isActive;
		public bool IsActive
		{
			get { return _isActive; }
			set
			{
				if(_isActive != value)
				{
					_isActive = value;
					RaisePropertyChanged(() => IsActive);
				}
			}
		}
		

		public CommandItem()
		{
			IconSize = "Small";
			SizeDefinition = "Middle";
		}

	}

}
