using System.Xml.Linq;

namespace SubmitInstance
{
	internal interface IInstancePoster
	{
		/// <param name="param"></param>
		/// <returns>Response</returns>
		Task<XDocument> Post(string server, string action, SubmitInstanceDataRequest param);
	}
}
