using SimpleSOAPClient;
using SimpleSOAPClient.Helpers;
using SimpleSOAPClient.Models;
using System.Xml.Linq;

namespace SubmitInstance
{
	internal class InstancePoster : IInstancePoster
	{
		public async Task<XDocument> Post(string server, string action, XDocument body)
		{
			SoapClient client = SoapClient.Prepare();
			SoapEnvelope requestEnvelope = SoapEnvelope.Prepare().Body(body.Root);
			SoapEnvelope responseEnvelope = await client.SendAsync(server, action, requestEnvelope);
			return new XDocument(responseEnvelope.Body.Value);
		}
	}
}
