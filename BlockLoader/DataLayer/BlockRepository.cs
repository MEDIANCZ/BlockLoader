using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace BlockLoader.DataLayer
{
	public class BlockRepository : IBlockRepository
	{
		private const string BlockElementName = "block";
		private const string CodeElementName = "code";
		private const string FootageElementName = "footage";
		private const string ProgramElementName = "program";
		private readonly XmlLoader _loader;
		private readonly string _filePath;

		public BlockRepository(XmlLoader loader, string filePath)
		{
			_loader = loader;
			_filePath = filePath;
		}

		public IEnumerable<Block> LoadBlocks()
		{
			if (!File.Exists(_filePath))
			{
				throw new FileNotFoundException(_filePath);
			}

			var doc = LoadDocument(_filePath);
			if (doc?.Root == null)
			{
				throw new InvalidOperationException("Xml file is empty, or invalid.");
			}

			var blockElements = doc.Root.Elements(BlockElementName);

			return blockElements.Select(CreateBlockFromElement).Where(b => b != null);
		}

		private Block CreateBlockFromElement(XElement blockElement)
		{
			var attributes = blockElement.Attributes().ToDictionary(e => e.Name.LocalName);
			if (!attributes.ContainsKey(CodeElementName) || !attributes.ContainsKey(FootageElementName) || !attributes.ContainsKey(ProgramElementName))
			{
				throw new InvalidOperationException("Block is missing required attribute.");
			}

			var code = attributes[CodeElementName].Value;
			var program = attributes[ProgramElementName].Value;
			var footage = attributes[FootageElementName].Value;

			if (string.IsNullOrWhiteSpace(code) || !int.TryParse(footage, out var footageNumber))
			{
				throw new FormatException("Footage must be a number.");
			}

			return new Block(code, footageNumber, program);

		}

		private XDocument LoadDocument(string filePath)
		{
			return _loader.Load(filePath);
		}
	}
}