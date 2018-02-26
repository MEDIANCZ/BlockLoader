namespace BlockLoader.DataLayer
{
	public class Respondent
	{
		public Respondent(string id, string[] reachedBlockCodes)
		{
			Id = id;
			ReachedBlockCodes = reachedBlockCodes;
		}

		public string Id { get; private set; }

		public string[] ReachedBlockCodes { get; private set; }
	}
}