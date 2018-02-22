using System.Collections.Generic;

namespace BlockLoader.DataLayer
{
	public interface IBlockRepository
	{
		IEnumerable<Block> LoadBlocks();
	}
}