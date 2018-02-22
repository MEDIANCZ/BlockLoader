using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace BlockLoader.Utils
{
	public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
	{
		public virtual event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged<TProperty>(Expression<Func<TProperty>> projection)
		{
			var memberExpression = (MemberExpression)projection.Body;
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
			}
		}
	}
}