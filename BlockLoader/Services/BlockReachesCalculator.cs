using System.Collections.Generic;
using System.Linq;
using BlockLoader.DataLayer;

namespace BlockLoader.Services
{
	internal class BlockReachesCalculator
	{
		public Dictionary<string, int> CalculateBlockReaches(IEnumerable<Respondent> respondents)
		{
			return respondents
				.SelectMany(
					r => r.ReachedBlockCodes.Select(
						bc => new
						      {
							      Respondent = r.Id,
							      BlockCode = bc
						      }))
				.GroupBy(x => x.BlockCode)
				.ToDictionary(g => g.Key, g => g.Distinct().Count());
		}
	}
}