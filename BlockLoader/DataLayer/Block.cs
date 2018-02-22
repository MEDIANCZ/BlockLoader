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

		public string Code { get; private set; }
		public int Footage { get; private set; }
		public string Program { get; private set; }
	}
}