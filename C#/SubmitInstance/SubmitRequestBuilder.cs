using System.Xml.Linq;

namespace SubmitInstance
{
	public interface ISubmitRequestBuilder
	{
		XDocument Build(SubmitParam param, string submitNamespace, string content);
	}
}
