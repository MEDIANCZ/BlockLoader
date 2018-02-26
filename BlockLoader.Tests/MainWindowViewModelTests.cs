using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlockLoader.DataLayer;
using BlockLoader.PresentationLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlockLoader.Tests
{
	[TestClass]
	public class MainWindowViewModelTests
	{
		private IList<Block> _blocks;
		private IList<Respondent> _respondents;
		private MainWindowViewModel _mainWindowViewModel;

		[TestInitialize]
		public void Setup()
		{
			_blocks = new List<Block>();
			_respondents = new List<Respondent>();

			_mainWindowViewModel = new MainWindowViewModel(
				new BlockRepositoryFake(_blocks),
				new RespondentRepositoryFake(_respondents));

			_blocks.Add(new Block("a", 20, "Neváhej a toč"));
			_blocks.Add(new Block("b", 25, "Neváhej a koukej"));
			_blocks.Add(new Block("c", 35, "Neváhej a padej"));
		}

		[TestMethod]
		public async Task LoadBlocks_AllBlocksLoaded()
		{
			await _mainWindowViewModel.LoadBlocks();

			Assert.AreEqual(_blocks.Count, _mainWindowViewModel.Blocks.Count);

			Assert.AreEqual(_blocks[0].Program, _mainWindowViewModel.Blocks[0].Program);
			Assert.AreEqual(_blocks[1].Program, _mainWindowViewModel.Blocks[1].Program);
			Assert.AreEqual(_blocks[2].Program, _mainWindowViewModel.Blocks[2].Program);
		}

		[TestMethod]
		public async Task CalculateReaches_UpdateReachedRespondentsCount()
		{
			_respondents.Add(new Respondent("1", new[] { "a", "b" }));
			_respondents.Add(new Respondent("2", new[] { "c" }));
			_respondents.Add(new Respondent("3", new[] { "a" }));

			await _mainWindowViewModel.LoadBlocks();
			await _mainWindowViewModel.CalculateReaches();

			Assert.AreEqual(2, _mainWindowViewModel.Blocks.Single(b => b.Code == "a").ReachedRespondentsCount);
			Assert.AreEqual(1, _mainWindowViewModel.Blocks.Single(b => b.Code == "b").ReachedRespondentsCount);
			Assert.AreEqual(1, _mainWindowViewModel.Blocks.Single(b => b.Code == "c").ReachedRespondentsCount);
		}
	}
}