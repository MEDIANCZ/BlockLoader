using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BlockLoader.DataLayer;
using BlockLoader.Properties;
using BlockLoader.Utils;

namespace BlockLoader.PresentationLayer
{
	public class MainWindowViewModel : NotifyPropertyChangedBase
	{
		private readonly IBlockRepository _blockRepository;
		private bool _isBusy;
		private bool _isGridVisible;

		public MainWindowViewModel(IBlockRepository blockRepository)
		{
			_blockRepository = blockRepository;
			IsGridVisible = false;
			Blocks = new ObservableCollection<BlockViewModel>();
			LoadBlocksCommand = new AsyncDelegateCommand(LoadBlocks);
		}

		public ICommand LoadBlocksCommand { get; private set; }

		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				if (value == _isBusy)
				{
					return;
				}

				_isBusy = value;
				NotifyPropertyChanged(() => IsBusy);
			}
		}

		public ObservableCollection<BlockViewModel> Blocks { get; private set; }

		public bool IsGridVisible
		{
			get { return _isGridVisible; }
			set
			{
				if (value == _isGridVisible)
				{
					return;
				}

				_isGridVisible = value;
				NotifyPropertyChanged(() => IsGridVisible);
			}
		}

		public async Task LoadBlocks()
		{
			await DoBackgroundOperation(LoadBlocksInternal, OnLoadingBlocksError);
		}

		private async Task DoBackgroundOperation(Func<Task> operation, Action onException)
		{
			IsBusy = true;
			IsGridVisible = false;

			try
			{
				await operation();

				IsGridVisible = true;
			}
			catch (Exception)
			{
				onException();
			}
			finally
			{
				IsBusy = false;
			}
		}

		private static BlockViewModel CreateBlockViewModel(Block block)
		{
			return new BlockViewModel(block.Code, block.Footage, block.Program);
		}

		private void OnLoadingBlocksError()
		{
			MessageBox.Show(Resources.ErrorLoadingBlocks, Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
			Blocks.Clear();
		}

		private async Task LoadBlocksInternal()
		{
			var blocks = await Task.Run(() => _blockRepository.LoadBlocks());
			Blocks.Clear();
			foreach (var block in blocks)
			{
				Blocks.Add(CreateBlockViewModel(block));
			}
		}
	}
}