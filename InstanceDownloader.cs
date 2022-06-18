using SimpleSOAPClient;
using SimpleSOAPClient.Exceptions;
using SimpleSOAPClient.Helpers;
using SimpleSOAPClient.Models;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace GetInstance
{
	internal class InstanceDownloader : IInstanceDownloader
	{
		public async Task<string> Download(GetInstanceRequest param)
		{
			var requestEnvelope =
															SoapEnvelope.Prepare().Body(param);
			SoapEnvelope responseEnvelope = null;
			using (var client = new SoapHelper().GetInstance())
				responseEnvelope = await GetResponse(client, requestEnvelope);

			if (responseEnvelope == null) return null;
			else
				try
				{
					XPathNavigator nav = responseEnvelope.Body.Value.CreateNavigator();
					XmlNamespaceManager m = new(nav.NameTable);

					m.AddNamespace(string.Empty, "http://Cdr.Business.Workflow.Schemas.CdrServiceSubmitInstanceData");
					XPathNodeIterator it = nav.Evaluate("/GetInstanceDataResult/CdrServiceGetInstanceData") as XPathNodeIterator;

					if (it == null || !it.MoveNext())
					{
						Console.WriteLine("Live CDR Update Error B: ");
						return null;
					}
					else
					{
						return cdrXml = it.Current.Value.Replace("&gt;", ">").Replace("&lt;", "<");
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					return null;
				}
		}

		string PrepareMsg(string responseErrorMessage)
		{
			const int defaultErrorCode = 6;
			(string phrase, int code)[] errorPhrases = new[]
			{
																("not authorized",1 ),
																("not match",2 ),
																("Access denied",3 ),
																("ID_RSSD",4 )
												};

			StringBuilder sb = new();

			int errorCode = defaultErrorCode;
			for (int i = 0; i < errorPhrases.Length; i++)
				if (responseErrorMessage.IndexOf(errorPhrases[i].phrase) > 0)
					errorCode = i;

			sb.AppendLine("A problem was encountered while attempting to contact the CDR to download prior quarter history data.");
			switch (errorCode)
			{
				case 1:
				case 4:
				case 6:
					sb.AppendJoin("Error", errorCode, responseErrorMessage);
					break;
				case 2:
				case 3:
					sb.AppendLine("Invalid User Name or Password, or User is locked.");
					break;
			}
			sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.");
			return sb.ToString();
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
			catch (SoapEnvelopeSerializationException e)
			{
				//  Logger.LogError(e, $"Failed to serialize the SOAP Envelope [Envelope={e.Envelope}]");
				throw;
			}
			catch (SoapEnvelopeDeserializationException e)
			{
				//  Logger.LogError(e,   $"Failed to deserialize the response into a SOAP Envelope [XmlValue={e.XmlValue}]");
				throw;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Unable to connect to the Internet. Some firewalls require altering permissions to allow EasyCall Report to communicate with the Central Data Repository (CDR).  Your information technology department should be made aware that this communication uses HTTPS via port 443.");
				Console.WriteLine("Also, the FFIEC CDR system now only supports TLS 1.2;  please insure your operating system supports said.");
			}
			return null;
		}

	}
}
