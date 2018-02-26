using System.Collections.Generic;
using BlockLoader.DataLayer;

namespace BlockLoader.Tests
{
	internal class RespondentRepositoryFake : IRespondentRepository
	{
		private readonly IList<Respondent> _respondents;

		public RespondentRepositoryFake(IList<Respondent> respondents)
		{
			_respondents = respondents;
		}

		public IEnumerable<Respondent> LoadRespondents()
		{
			return _respondents;
		}
	}
}