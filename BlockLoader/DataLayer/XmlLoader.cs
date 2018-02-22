using System.Threading;
using System.Xml.Linq;

namespace BlockLoader.DataLayer
{
	public class XmlLoader
	{
		public XDocument Load(string filePath)
		{
			Thread.Sleep(2500); // simulujeme pomalé načtení velkých dat
			return XDocument.Load(filePath);
		}
	}
}