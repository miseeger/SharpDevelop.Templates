using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace ${SolutionName}.Base.Mvvm
{

	public class NotifyableObject : INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;


	    protected string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
		{
			if (propertyExpression == null)
			{
				throw new ArgumentNullException("propertyExpression");
			}

			var memberExpression = propertyExpression.Body as MemberExpression;
			if (memberExpression == null)
			{
				throw new ArgumentException("The expression is not a member access expression.", "propertyExpression");
			}

			var property = memberExpression.Member as PropertyInfo;
			if (property == null)
			{
				throw new ArgumentException("The member access expression does not access a property.", "propertyExpression");
			}

			var getMethod = property.GetGetMethod(true);
			if (getMethod == null)
			{
				throw new ArgumentException("The referenced property does not have a get method.", "propertyExpression");
			}

			if (getMethod.IsStatic)
			{
				throw new ArgumentException("The referenced property is a static property.", "propertyExpression");
			}

			return memberExpression.Member.Name;
		}


		protected virtual void RaisePropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}


		protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
		{
			var propertyName = ExtractPropertyName(propertyExpression);
			RaisePropertyChanged(propertyName);
		}

	}

}