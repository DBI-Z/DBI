using System.Xml.Linq;

namespace SubmitInstance
{
	internal interface IInstancePoster
	{
		Task<XDocument> Post(string server, string action, SubmitInstanceDataRequest param);
	}
}
