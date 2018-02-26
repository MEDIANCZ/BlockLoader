using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace BlockLoader.DataLayer
{
	public abstract class RepositoryBase<T>
	{
		private readonly XmlLoader _loader;
		private readonly string _filePath;

		protected RepositoryBase(XmlLoader loader, string filePath)
		{
			_loader = loader;
			_filePath = filePath;
		}

		protected IEnumerable<T> Load()
		{
			if (!File.Exists(_filePath))
			{
				throw new FileNotFoundException(_filePath);
			}

			var doc = LoadDocument(_filePath);
			if (doc == null || doc.Root == null)
			{
				throw new InvalidOperationException("Xml file is empty, or invalid.");
			}

			return doc.Root.Elements(ElementName).Select(Parse).Where(b => b != null);
		}

		protected abstract string ElementName { get; }

		protected abstract T Parse(XElement element);

		private XDocument LoadDocument(string filePath)
		{
			return _loader.Load(filePath);
		}
	}
}