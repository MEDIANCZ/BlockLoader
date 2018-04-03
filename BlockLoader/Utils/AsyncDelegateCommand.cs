using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlockLoader.Utils
{
	public class AsyncDelegateCommand : ICommand
	{
		private readonly Func<Task> _command;
		private bool _canExecute = true;

		public AsyncDelegateCommand(Func<Task> command)
		{
			_command = command;
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		protected void RaiseCanExecuteChanged()
		{
			CommandManager.InvalidateRequerySuggested();
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute;
		}

		public async void Execute(object parameter)
		{
			_canExecute = false;
			await _command();
			_canExecute = true;
			RaiseCanExecuteChanged();
		}
	}
}