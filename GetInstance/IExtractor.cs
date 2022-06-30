using System.Xml.Linq;

namespace GetInstance
{
	public interface IExtractor
	{
		List<WriteFormat> Extract(XDocument xbrl);
	}
}
