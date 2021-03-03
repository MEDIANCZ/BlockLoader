namespace BlockLoader.DataLayer
{
	public class Block
	{
		public Block(string code, int footage, string program)
		{
			Code = code;
			Footage = footage;
			Program = program;
		}

		public string Code { get; }
		public int Footage { get; }
		public string Program { get; }
	}
}