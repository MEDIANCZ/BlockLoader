using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BlockLoader.DataLayer
{
	public class BlockRepository : RepositoryBase<Block>, IBlockRepository
	{
		private const string BlockElementName = "block";
		private const string CodeElementName = "code";
		private const string FootageElementName = "footage";
		private const string ProgramElementName = "program";

		public BlockRepository(XmlLoader loader, string filePath) : base(loader, filePath)
		{
		}

		protected override string ElementName
		{
			get { return BlockElementName; }
		}

		public IEnumerable<Block> LoadBlocks()
		{
			return Load();
		}

		protected override Block Parse(XElement element)
		{
			var attributes = element.Attributes().ToDictionary(e => e.Name.LocalName);
			if (!attributes.ContainsKey(CodeElementName) || !attributes.ContainsKey(FootageElementName) || !attributes.ContainsKey(ProgramElementName))
			{
				throw new InvalidOperationException("Block is missing required attribute.");
			}

			var code = attributes[CodeElementName].Value;
			var program = attributes[ProgramElementName].Value;
			var footage = attributes[FootageElementName].Value;

			int footageNumber;
			if (string.IsNullOrWhiteSpace(code) || !int.TryParse(footage, out footageNumber))
			{
				throw new FormatException("Footage must be a number.");
			}

			return new Block(code, footageNumber, program);
		}
	}
}