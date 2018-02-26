using System.Windows;
using BlockLoader.DataLayer;
using BlockLoader.PresentationLayer;

namespace BlockLoader
{
	public partial class App : Application
	{
		private const string BlocksFileName = @"Data\Blocks.xml";
		private const string RespondentsFileName = @"Data\Respondents.xml";

		protected override void OnStartup(StartupEventArgs e)
		{
			//Composition root:
			var xmlLoader = new XmlLoader();
			var blockRepository = new BlockRepository(xmlLoader, BlocksFileName);
			var respondentsRepository = new RespondentRepository(xmlLoader, RespondentsFileName);
			var window = new MainWindowView
			             {
				             DataContext = new MainWindowViewModel(blockRepository, respondentsRepository)
			             };
			window.Show();
			base.OnStartup(e);
		}
	}
}