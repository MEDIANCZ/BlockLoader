using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlockLoader.Utils
{
	public class AsyncDelegateCommand : ICommand
	{
		private readonly Func<Task> _command;
		private readonly Func<bool> _canExecute;
		private bool _executing;

		public AsyncDelegateCommand(Func<Task> command, Func<bool> canExecute = null)
		{
			_command = command;
			_canExecute = canExecute;
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
			return !_executing && (_canExecute == null || _canExecute());
		}

		public async void Execute(object parameter)
		{
			_executing = true;
			await _command();
			_executing = false;
		}
	}
}