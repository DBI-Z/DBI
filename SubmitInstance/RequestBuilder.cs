using System.Xml.Linq;

namespace SubmitInstance
{
	public class RequestBuilder : ISubmitRequestBuilder
	{
		public XDocument Build(SubmitParam param, string submitNamespace, string content)
		{
			string submitElementName = param.Prod ? Settings.SubmitRequestProd : Settings.SubmitRequestTest;

			XDocument envelopeBody =
				new XDocument(
					new XElement(XName.Get(submitElementName, submitNamespace)
					, new XElement[]
					{
						new XElement(XName.Get("userName",submitNamespace), param.Username),
						new XElement(XName.Get("password",submitNamespace), param.Password),
						new XElement(XName.Get("instanceData", submitNamespace), content)
					}));

			return envelopeBody;
		}
	}
}