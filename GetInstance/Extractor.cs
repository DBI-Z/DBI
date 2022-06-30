using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace GetInstance
{
	public class Extractor : IExtractor
	{
		public List<WriteFormat> Extract(XDocument xbrl)
		{
			XPathNodeIterator ccRecordsIterator = GetCCIterator(xbrl);
			List<WriteFormat> wf = DeserializeRecords(ccRecordsIterator);
			return wf;
		}
		XPathNodeIterator GetCCIterator(XDocument doc)
		{
			XPathNavigator n = doc.CreateNavigator();
			XmlNamespaceManager m = new XmlNamespaceManager(n.NameTable);
			m.AddNamespace("cc", "http://www.ffiec.gov/xbrl/call/concepts");
			return n.Evaluate("//cc:*", m) as XPathNodeIterator;
		}
		List<WriteFormat> DeserializeRecords(XPathNodeIterator ccElements)
		{
			List<WriteFormat> wf = new();

			while (ccElements.MoveNext())
			{
				var mtContextRef = ccElements.Current.GetAttribute("contextRef", string.Empty);

				if (ccElements.Current.MoveToChild(XPathNodeType.Text))
				{
					bool isConceptDataRecord = bool.TryParse(ccElements.Current.Value, out bool mtBool);
					string? mtDecimals = ccElements.Current.GetAttribute("decimals", string.Empty);
					if (isConceptDataRecord)
					{
						ccElements.Current.MoveToParent();
						Console.WriteLine(mtContextRef + " -> " + mtBool.ToString());

						wf.Add(new WriteFormat
						{
							MtMdrm = ccElements.Current.LocalName,
							MtData = mtBool ? "true" : "false",
							MtDecimals = "0",
							MtContextRef = mtContextRef
						}) ;
					}
					else
					{
						ccElements.Current.MoveToParent();
						string? mtText = ccElements.Current.Value;
						string? mtMdrm = ccElements.Current.LocalName;
						string? mtUnitRef = ccElements.Current.GetAttribute("unitRef", string.Empty);

						wf.Add(new WriteFormat
						{
							MtDecimals = mtDecimals,
							MtData = mtText,
							MtMdrm = mtMdrm,
							MtUnitRef = mtUnitRef,
							MtContextRef = mtContextRef
						});
					}
				}
				else
				{
					Console.WriteLine("no text child node?");
				}
			}
			return wf;
		}
	}
}