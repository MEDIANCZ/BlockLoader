using System.Threading.Tasks;
using BlockLoader.DataLayer;
using BlockLoader.PresentationLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlockLoader.Tests
{
	[TestClass]
	public class MainWindowViewModelTests
	{
		[TestMethod]
		public async Task LoadBlocks_AllBlocksLoaded()
		{
			var blocks = new[]
			             {
				             new Block("a", 20, "Neváhej a toč"),
				             new Block("b", 25, "Neváhej a koukej"),
				             new Block("c", 35, "Neváhej a padej")
			             };
			var mainWindowViewModel = new MainWindowViewModel(new BlockRepositoryFake(blocks));
			await mainWindowViewModel.LoadBlocks();

			Assert.AreEqual(blocks.Length, mainWindowViewModel.Blocks.Count);

			Assert.AreEqual(blocks[0].Program, mainWindowViewModel.Blocks[0].Program);
			Assert.AreEqual(blocks[1].Program, mainWindowViewModel.Blocks[1].Program);
			Assert.AreEqual(blocks[2].Program, mainWindowViewModel.Blocks[2].Program);
		}
	}
}