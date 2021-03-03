using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace BlockLoader.Utils
{
	public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
	{
		public virtual event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged<TProperty>(Expression<Func<TProperty>> projection)
		{
			var memberExpression = (MemberExpression)projection.Body;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
		}

		protected void NotifyPropertyChanged<TProperty>([CallerMemberName]string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}