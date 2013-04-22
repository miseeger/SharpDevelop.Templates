using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Base.Mvvm.Interfaces;

namespace ${SolutionName}.Base.Mvvm
{

	public abstract class ViewModelBase : NotifyableObject, IViewModel
	{

		private IView _view;
		public IView View { 
			get { return _view; }
			set 
			{
				_view = value;
				_view.ViewModel = this;
			}
		}
		
		
		private IViewDialog _viewDialog;
		public IViewDialog ViewDialog { 
			get { return _viewDialog; }
			set 
			{
				_viewDialog = value;
				_viewDialog.ViewModel = this;
			}
		}


		private bool _isDirty;
		public bool IsDirty
		{
			get { return _isDirty; }
			set
			{
				if (_isDirty != value)
				{
					_isDirty = value;
					RaisePropertyChanged(() => IsDirty);
				}
			}
		}


		private bool _isBusy;
		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				if (_isBusy != value)
				{
					_isBusy = value;
					RaisePropertyChanged(() => IsBusy);
				}
			}
		}


		public Boolean InDesign { get; private set; }


		private string _vmTitle;
		public string VmTitle
		{
			get { return _vmTitle; }
			set
			{
				if(_vmTitle != value)
				{
					_vmTitle = value;
					RaisePropertyChanged(() => VmTitle);
				}
			}
		}
		
		
		private ImageSource _vmImage;
		public ImageSource VmImage
		{
			get { return _vmImage; }
			set
			{
				if(_vmImage != value)
				{
					_vmImage = value;
					RaisePropertyChanged(() => VmImage);
				}
			}
		}



		public ViewModelBase()
		{

			PropertyChanged += (o, e) =>
			{ 
				if (!Names.IsDrityIgnoringProperties.Contains(e.PropertyName) 
				    && !IsDirty)
				{
					IsDirty = true;	
				}
			};

			InDesign = (bool)DependencyPropertyDescriptor
				.FromProperty(DesignerProperties.IsInDesignModeProperty,
					typeof(FrameworkElement)).Metadata.DefaultValue;
		}


		public ViewModelBase(IView view) : this()
		{
			if (view != null)
			{
				View = view;
				View.ViewModel = this;
			}
		}


		public virtual void Initialize()
		{
		}


		public override string ToString()
		{
			return string.Format("ViewModel {0}", GetType().Name);
		}

	}
}
