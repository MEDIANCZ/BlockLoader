using System.Collections.Generic;
using BlockLoader.DataLayer;

namespace BlockLoader.Tests
{
	internal class BlockRepositoryFake : IBlockRepository
	{
		private readonly IList<Block> _blocks;

		public BlockRepositoryFake(IList<Block> blocks)
		{
			_blocks = blocks;
		}

		public IEnumerable<Block> LoadBlocks()
		{
			return _blocks;
		}
	}
}