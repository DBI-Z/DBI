using System.Xml.Linq;

namespace SubmitInstance
{
	public interface IInstancePoster
	{
		Task<XDocument> Post(string server, string action, XDocument param);
	}
}
