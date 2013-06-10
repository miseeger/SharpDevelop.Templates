using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using ${SolutionName}.Base.Mvvm.Interfaces;
using ${SolutionName}.Base.Mvvm.Events;

namespace ${SolutionName}.Base.Mvvm
{

	public class ValidatableDataObject: EditableDataObject, IValidatableDataObject
	{

		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

		private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();


		public List<string> ErrorStringlist
		{
			get
			{
				List<string> errorList = new List<string>();
				PropertyInfo[] properties = GetType().GetProperties();

				foreach (PropertyInfo pi in properties)
				{
					var error = this[pi.Name];
					if (!String.IsNullOrEmpty(error))
					{
						errorList.Add(error);
					}
				}

				return errorList;
			}
		}


		public string Errorlist
		{
			get
			{
				return ErrorStringlist.Aggregate((lst, err) => lst + "\n" + err);
			}
		}


		public bool IsValid
		{
			get 
			{
				return !HasErrors; 
			}
		}


		// ----- implementaion of IDataErrorInfo ------------------------------

		// never used -> always an empty string
		public string Error
		{
			get { return string.Empty; }
		}


		public virtual string this[string columnName]
		{
			get 
			{
				var errors = GetErrors(columnName);
				return errors != null ? errors.Cast<string>().Aggregate((lst, err) => lst + "\n" + err) : "";
			}
		}


		// ----- implementation of INotifyDataErrorInfo -----------------------

		public IEnumerable GetErrors(string propertyName)
		{
			if (_errors.ContainsKey(propertyName))
			{
				return _errors[propertyName];
			}
			return null;
		}


		public IEnumerable GetErrors<T>(Expression<Func<T>> propertyExpression)
		{
			var propertyName = ExtractPropertyName(propertyExpression);
			return GetErrors(propertyName);
		}


		public bool HasErrors
		{
			get 
			{
				return (_errors.Count > 0);
			}
		}


		public void AddError(string propertyName, string error)
		{
			if (!_errors.ContainsKey(propertyName))
			{
				_errors[propertyName] = new List<string>();
			}

			if (!_errors[propertyName].Contains(error))
			{
				_errors[propertyName].Add(error);
			}

			NotifyErrorsChanged(propertyName);
		}


		public void AddError<T>(Expression<Func<T>> propertyExpression, string error)
		{
			var propertyName = ExtractPropertyName(propertyExpression);
			AddError(propertyName, error);
		}


		public void RemoveError(string propertyName)
		{
			if (_errors.ContainsKey(propertyName))
			{
				_errors.Remove(propertyName);
			}
			NotifyErrorsChanged(propertyName);
		}


		public void RemoveError<T>(Expression<Func<T>> propertyExpression)
		{
			var propertyName = ExtractPropertyName(propertyExpression);
			RemoveError(propertyName);
		}


		public void NotifyErrorsChanged(string propertyName)
		{
			if (ErrorsChanged != null)
			{
				ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
			}
		}


		public void NotifyErrorsChanged<T>(Expression<Func<T>> propertyExpression)
		{
			var propertyName = ExtractPropertyName(propertyExpression);
			NotifyErrorsChanged(propertyName);
		}

	}

}