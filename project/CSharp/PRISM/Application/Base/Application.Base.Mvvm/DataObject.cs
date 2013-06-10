using System.Linq;
using ${SolutionName}.Base.Mvvm.Attributes;
using ${SolutionName}.Base.Mvvm.Interfaces;
using ${SolutionName}.Base.Mvvm.Enums;
 
namespace ${SolutionName}.Base.Mvvm
{ 
 
	public class DataObject: NotifyableObject, IDataObject 
	{ 
 
		protected DataObject() 
		{ 
			PropertyChanged += (o, e) => 
			{ 
				if (IgnoreObjectStatus 
				    && !Names.IsDrityIgnoringProperties.Contains(e.PropertyName)
				    && !IsDirty)
				{
					IsDirty = true;	
				}
				else 
				{
					if (ObjectStatus == DataObjectStatus.Original
				    	&& (!Names.ObjectStatusIgnoringProperties.Contains(e.PropertyName)))
					{ 
						ObjectStatus = DataObjectStatus.Modified; 
					} 
				}
			};
			
			var classAttributes = (IgnoreObjectStatusAttribute[])GetType()
				.GetCustomAttributes(typeof(IgnoreObjectStatusAttribute), false);
			IgnoreObjectStatus = (classAttributes.Any() && classAttributes[0].IgnoreObjectStatus);
			
			ObjectStatus = IgnoreObjectStatus ? DataObjectStatus.Ignore : DataObjectStatus.Original;
		}
		
		
		public bool _ignoreObjectStatus;
		public bool IgnoreObjectStatus 
		{
			get { return _ignoreObjectStatus; }
			set {
					if (_ignoreObjectStatus != value) 
					{
						_ignoreObjectStatus = value;
						
						if (value)
						{
							ObjectStatus = DataObjectStatus.Ignore;
						}
						else
						{
							ObjectStatus = IsDirty ? DataObjectStatus.Modified : DataObjectStatus.Original;
						}
					}
				}
		}

		
		private DataObjectStatus _objectStatus = DataObjectStatus.Original;
		public DataObjectStatus ObjectStatus 
		{ 
			get { return _objectStatus; } 
			set 
			{ 
				if (IgnoreObjectStatus) 
				{
					_objectStatus = DataObjectStatus.Ignore;
				}
				else
				{
					if (_objectStatus != value)
				 	{ 
						_objectStatus = value;
						IsDirty = (_objectStatus != DataObjectStatus.Original && _objectStatus != DataObjectStatus.Ignore);
						RaisePropertyChanged(() => ObjectStatus);
					}
				}
			} 
		}


		private bool _isDirty; 
		public bool IsDirty 
		{ 
			get { return _isDirty; } 
			set 
			{
				if (IgnoreObjectStatus) 
				{
					if (_isDirty != value)
					{
						_isDirty = value;
						RaisePropertyChanged(() => IsDirty);
					}
				}
				else
				{
					if ((ObjectStatus != DataObjectStatus.Added && _isDirty != value)
				    	|| (ObjectStatus == DataObjectStatus.Added && _isDirty != value && value))
					{	
						_isDirty = value;
						RaisePropertyChanged(() => IsDirty);
						
						if (_isDirty && ObjectStatus == DataObjectStatus.Original)
						{
							ObjectStatus = DataObjectStatus.Modified;
						}
						else
						{
							if (!_isDirty 
							    && (ObjectStatus == DataObjectStatus.Modified
							        || ObjectStatus == DataObjectStatus.Deleted))
							{
								ObjectStatus = DataObjectStatus.Original;
							}
						}
					}
				}
			}
		} 
 
 
		public void ResetStatus() 
		{ 
			if (IgnoreObjectStatus) 
			{
				IsDirty = false;
			} else {
				ObjectStatus = DataObjectStatus.Original;	
			}
		} 
 
	} 
 
} 
