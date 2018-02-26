using BlockLoader.Utils;

namespace BlockLoader.PresentationLayer
{
	public class BlockViewModel : NotifyPropertyChangedBase
	{
		private int _reachedRespondentsCount;

		public BlockViewModel(string code, int footage, string program)
		{
			Code = code;
			Footage = footage;
			Program = program;
		}

		public string Code { get; private set; }
		public int Footage { get; private set; }
		public string Program { get; private set; }

		public int ReachedRespondentsCount
		{
			get { return _reachedRespondentsCount; }
			set
			{
				_reachedRespondentsCount = value;
				NotifyPropertyChanged(() => ReachedRespondentsCount);
			}
		}
	}
}