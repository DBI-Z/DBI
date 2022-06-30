using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace GetInstance
{
	public class InstanceGetter
	{
		IInstanceWriter writer;
		IInstanceDownloader downloader;
		IExtractor extractor;

		public InstanceGetter(IInstanceDownloader downloader, IInstanceWriter writer, IExtractor extractor)
		{
			this.writer = writer;
			this.downloader = downloader;
			this.extractor = extractor;
		}

		public async Task Get(GetInstanceRequest param)
		{
			string appFolder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			XDocument responseBody = await downloader.Download(param);

			Console.WriteLine(responseBody.ToString());
			GetInstanceResponse response = new Cdr().ExtractResponse(responseBody);
			bool IsSuccessfulGetInstance = response.Code == 0; 
			if (IsSuccessfulGetInstance)
			{
				Console.WriteLine("3. Processing CDR Data: ");

				IEnumerable<XElement> ins = response.InstanceDocuments.Element(XName.Get("InstanceDocuments")).Elements(XName.Get("InstanceDocument"));
					List<WriteFormat> wf = new();
				foreach (XElement instanceDocument in ins)
				{
					XDocument xbrl = new XDocument(instanceDocument.Element(XName.Get("xbrl", "http://www.xbrl.org/2003/instance")));
					string period = GetPeriod(xbrl);


					if (DateTime.TryParseExact(period, GetInstanceRequest.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime xbrlEndTime))
					{
						if (xbrlEndTime.Date == param.ReportingPeriodEndDate.Date)
						{
							Console.WriteLine($"Skipping processing of current period: {period}");
							continue;
						}
					}
					else
					{
						Console.WriteLine($"Cannot read period as date: {period}");
					}
					var thisWf = extractor.Extract(xbrl);
					wf.AddRange(thisWf);
				}
				string currentPeriod = param.ReportingPeriodEndDate.ToString(GetInstanceRequest.DateFormat);
				string instanceFileName = Path.Combine(appFolder, currentPeriod + "-Instance.txt");
				using (FileStream instanceFile = new(instanceFileName, FileMode.Create))
					writer.Write(records: wf, instanceFile);
				Console.WriteLine("Completed");
				Console.WriteLine("CDR Live Update: Successful");
				Console.WriteLine("Prior quarter history data has been downloaded successfully.");
			}
			else
			{
				string interpretedErrorMessage = PrepareMsg(responseBody.ToString());
				AskPreviousFile("2022-Instance.txt");
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

		void AskPreviousFile(string instanceFileName)
		{
			if (File.Exists(instanceFileName))
			{
				Console.WriteLine("Although the Live CDR Update was not successful, prior quarter data is available on your hard drive.");
				Console.WriteLine("It should be safe to use this data in verifying your Call Report.");
				Console.WriteLine("Do you want to continue Updating History with this prior quarter data? Y/N");

				string yn = string.Empty;
				Func<bool> isaYes = () => string.Compare("Y", yn?.Trim(), ignoreCase: true) == 0;
				Func<bool> isaNo = () => string.Compare("N", yn?.Trim(), ignoreCase: true) == 0;

				while (!isaYes())
				{
					yn = Console.ReadLine();
					if (isaNo())
					{
						Console.WriteLine("Update History was not completed.");
						Console.WriteLine("Live CDR Update Error");
						break;
					}
				}
			}
			else
			{
				Console.WriteLine("Update History was not completed.");
			}
		}

		private string GetPeriod(XDocument xbrlRoot)
		{
			const string defaultNamespace = "http://www.xbrl.org/2003/instance";
			if (xbrlRoot.Element(XName.Get("xbrl", defaultNamespace)) is XElement xbrl)
				foreach (XElement context in xbrl.Elements(XName.Get("context", defaultNamespace)))
					if (context.Element(XName.Get("period", defaultNamespace))?.Element(XName.Get("endDate", defaultNamespace))?.Value is string endDate)
						return endDate;
			return null;
		}
	}

	[XmlRoot("GetInstanceData", Namespace = "http://ffiec.gov/cdr/services/")]
	public class GetInstanceRequest
	{
		public const string DateFormat = "yyyy-MM-dd";
		[XmlElement("userName")]
		public string Username { get; set; }
		[XmlElement("password")]
		public string Password { get; set; }
		[XmlElement("dataSeriesName")]
		public string DataSeriesName { get; set; }
		[XmlElement("reportingPeriodEndDate")]
		public DateTime ReportingPeriodEndDate { get; set; }
		[XmlElement("id_rssd")]
		public string IdRssd { get; set; }
		[XmlElement("numberOfPriorPeriods")]
		public int NumberOfPriorPeriods { get; set; }
	}
}
