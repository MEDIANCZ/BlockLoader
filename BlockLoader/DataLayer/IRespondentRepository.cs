using System.Collections.Generic;

namespace BlockLoader.DataLayer
{
	public interface IRespondentRepository
	{
		IEnumerable<Respondent> LoadRespondents();
	}
}