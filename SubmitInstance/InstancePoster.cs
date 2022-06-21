using SimpleSOAPClient;
using SimpleSOAPClient.Helpers;
using SimpleSOAPClient.Models;
using System.Xml.Linq;

namespace SubmitInstance
{
	internal class InstancePoster : IInstancePoster
	{
		public async Task<XDocument> Post(string server, string action, SubmitInstanceDataRequest param)
		{
			var client = SoapClient.Prepare();
			var requestEnvelope = SoapEnvelope.Prepare().Body(param);
			var responseEnvelope = await client.SendAsync(server, action, requestEnvelope);
			return new XDocument(responseEnvelope.Body.Value);
		}
	}
}
