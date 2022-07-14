using SimpleSOAPClient;
using SimpleSOAPClient.Helpers;
using SimpleSOAPClient.Models;
using System.Xml.Linq;

namespace GetInstance
{
	internal class Prior2Downloader : IInstanceDownloader
	{
		IDisplayer displayer;

		public Prior2Downloader(IDisplayer displayer)
		{
			this.displayer = displayer;
		}
		public async Task<XDocument> Download(GetInstanceRequest param)
		{
			GetInstanceRequest prior2 = new()
			{
				NumberOfPriorPeriods = param.NumberOfPriorPeriods + 2,
				DataSeriesName = param.DataSeriesName,
				IdRssd = param.IdRssd,
				Password = param.Password,
				ReportingPeriodEndDate = param.ReportingPeriodEndDate,
				Username = param.Username,
			};

			var requestEnvelope = SoapEnvelope.Prepare().Body(prior2);
			SoapEnvelope responseEnvelope = null;
			using (var client = new SoapHelper().GetInstance())
				responseEnvelope = await GetResponse(client, requestEnvelope);
			if (responseEnvelope == null)
				return new XDocument();
			else 
				return new XDocument(responseEnvelope.Body.Value);
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
				displayer.WriteLine(ex.Message);
				displayer.WriteLine("Unable to connect to the Internet. Some firewalls require altering permissions to allow EasyCall Report to communicate with the Central Data Repository (CDR).  Your information technology department should be made aware that this communication uses HTTPS via port 443.");
				displayer.WriteLine("Also, the FFIEC CDR system now only supports TLS 1.2;  please insure your operating system supports said.");
			}
			return null;
		}

	}
}
