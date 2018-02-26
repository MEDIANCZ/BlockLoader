using System;
using System.Collections.ObjectModel;
using System.Linq;
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
		private readonly IRespondentRepository _respondentsRepository;

		public MainWindowViewModel(IBlockRepository blockRepository, IRespondentRepository respondentsRepository)
		{
			_blockRepository = blockRepository;
			_respondentsRepository = respondentsRepository;
			IsGridVisible = false;
			Blocks = new ObservableCollection<BlockViewModel>();
			LoadBlocksCommand = new AsyncDelegateCommand(LoadBlocks);
			CalculateReachesCommand = new AsyncDelegateCommand(CalculateReaches, () => Blocks.Any());
		}

		public ICommand LoadBlocksCommand { get; private set; }

		public ICommand CalculateReachesCommand { get; private set; }

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

		public async Task CalculateReaches()
		{
			await DoBackgroundOperation(
				CalculateReachesInternal,
				() => MessageBox.Show(Resources.ErrorCalculatingReaches, Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error));
		}

		private async Task CalculateReachesInternal()
		{
			var respondents = await Task.Run(() => _respondentsRepository.LoadRespondents());

			var blockReaches =
				respondents
					.SelectMany(
						r => r.ReachedBlockCodes.Select(
							bc => new
							      {
								      Respondent = r.Id,
								      BlockCode = bc
							      }))
					.GroupBy(x => x.BlockCode)
					.ToDictionary(g => g.Key, g => g.Distinct().Count());

			foreach (var blockViewModel in Blocks.Where(b => blockReaches.ContainsKey(b.Code)))
			{
				blockViewModel.ReachedRespondentsCount = blockReaches[blockViewModel.Code];
			}
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