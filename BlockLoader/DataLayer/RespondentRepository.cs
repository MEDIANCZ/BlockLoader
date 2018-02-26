using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BlockLoader.DataLayer
{
	public class RespondentRepository : RepositoryBase<Respondent>, IRespondentRepository
	{
		public RespondentRepository(XmlLoader xmlLoader, string respondentsFilePath) : base(xmlLoader, respondentsFilePath)
		{
		}

		public IEnumerable<Respondent> LoadRespondents()
		{
			return Load();
		}

		protected override string ElementName
		{
			get { return "respondent"; }
		}

		protected override Respondent Parse(XElement element)
		{
			return new Respondent(
				element.Attribute("id").Value,
				element
					.Element("reachedblocks")
					.Elements("reachedblock")
					.Select(GetBlockCode).ToArray());
		}

		private string GetBlockCode(XElement blockElement)
		{
			return blockElement.Attribute("code").Value;
		}
	}
}