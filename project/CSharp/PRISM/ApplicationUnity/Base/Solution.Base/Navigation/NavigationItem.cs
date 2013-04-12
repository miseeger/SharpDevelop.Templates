using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using ${SolutionName}.Base.Mvvm;

namespace ${SolutionName}.Base.Navigation
{

	public class NavigationItem : NotifyableObject
	{

		public string Caption { get; set; }
		public string NavigationPath { get; set; } 
		public ImageSource ItemImage { get; set; } 
		
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


		public NavigationItem()
		{
		}

	}

}
