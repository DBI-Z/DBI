using SimpleSOAPClient;
using SimpleSOAPClient.Exceptions;
using SimpleSOAPClient.Helpers;
using SimpleSOAPClient.Models;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace GetInstance
{
	internal class InstanceDownloader : IInstanceDownloader
	{
		public async Task<XDocument> Download(GetInstanceRequest param)
		{
			var requestEnvelope = SoapEnvelope.Prepare().Body(param);
			SoapEnvelope responseEnvelope = null;
			using (var client = new SoapHelper().GetInstance())
				responseEnvelope = await GetResponse(client, requestEnvelope);
			XDocument responseBody = new(responseEnvelope.Body.Value);
			return responseBody;
		} 

		async Task<SoapEnvelope> GetResponse(SoapClient client, SoapEnvelope requestEnv)
		{
			try
			{
				var responseEnv = await client.SendAsync(
"https://cdr.ffiec.gov/cdr/Services/CdrService.asmx",
"http://ffiec.gov/cdr/services/GetInstanceData", requestEnv, new CancellationToken());
				return responseEnv;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine("Unable to connect to the Internet. Some firewalls require altering permissions to allow EasyCall Report to communicate with the Central Data Repository (CDR).  Your information technology department should be made aware that this communication uses HTTPS via port 443.");
				Console.WriteLine("Also, the FFIEC CDR system now only supports TLS 1.2;  please insure your operating system supports said.");
			}
			return null;
		}

	}
}
