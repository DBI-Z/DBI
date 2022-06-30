using System.Diagnostics;
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

		public InstanceGetter(IInstanceDownloader downloader, IInstanceWriter writer)
		{
			this.writer = writer;
			this.downloader = downloader;
		}

		public async Task Do(GetInstanceRequest param)
		{
			string appFolder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			XDocument responseBody = await downloader.Download(param);

			Console.WriteLine(responseBody.ToString());
			GetInstanceResponse response = new Cdr().ExtractResponse(responseBody);
			bool IsSuccessfulGetInstance = response.Code == 0;
			IEnumerable<XDocument> instances = null;
			if (IsSuccessfulGetInstance)
			{
				Console.WriteLine("3. Processing CDR Data: ");

				IEnumerable<XElement> ins = response.InstanceDocuments.Element(XName.Get("InstanceDocuments")).Elements(XName.Get("InstanceDocument"));
				foreach (XElement instanceDocument in ins)
				{
					XDocument xbrl = new XDocument(instanceDocument.Element(XName.Get("xbrl", "http://www.xbrl.org/2003/instance")));
					string period = GetPeriod(xbrl);
					XPathNodeIterator ccRecordsIterator = GetCCIterator(xbrl);
					List<WriteFormat> wf = DeserializeRecords(ccRecordsIterator);

					string instanceFileName = Path.Combine(appFolder, period + "-Instance.txt");
					using (FileStream instanceFile = new(instanceFileName, FileMode.Create))
						writer.Write(records: wf, instanceFile);
					Console.WriteLine("Completed");
					Console.WriteLine("CDR Live Update: Successful");
					Console.WriteLine("Prior quarter history data has been downloaded successfully.");
				}
			}
			if (instances == null || instances.Count() == 0)
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

		XPathNodeIterator GetCCIterator(XDocument doc)
		{
			XPathNavigator n = doc.CreateNavigator();
			XmlNamespaceManager m = new XmlNamespaceManager(n.NameTable);
			m.AddNamespace("cc", "http://www.ffiec.gov/xbrl/call/concepts");
			return n.Evaluate("//cc:*", m) as XPathNodeIterator;
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

		List<WriteFormat> DeserializeRecords(XPathNodeIterator ccElements)
		{
			List<WriteFormat> wf = new();

			while (ccElements.MoveNext())
			{
				var mtContextRef = ccElements.Current.GetAttribute("contextRef", string.Empty);

				if (ccElements.Current.MoveToChild(XPathNodeType.Text))
				{
					bool isConceptDataRecord = bool.TryParse(ccElements.Current.Value, out bool mtBool);
					if (isConceptDataRecord)
					{
						ccElements.Current.MoveToParent();
						Console.WriteLine(mtContextRef + " -> " + mtBool.ToString());

						wf.Add(new WriteFormat
						{
							MtData = mtBool.ToString(),
							MtContextRef = mtContextRef
						}); ;
					}
					else
					{
						ccElements.Current.MoveToParent();
						string? mtText = ccElements.Current.Value;
						string? mtMdrm = ccElements.Current.LocalName;
						string? mtUnitRef = ccElements.Current.GetAttribute("unitRef", string.Empty);
						string? mtDecimals = ccElements.Current.GetAttribute("decimals", string.Empty);

						wf.Add(new WriteFormat
						{
							MtDecimals = mtDecimals,
							MtData = mtText,
							MtMdrm = mtMdrm,
							MtUnitRef = mtUnitRef,
							MtContextRef = mtContextRef
						});
					}
				}
				else
				{
					Console.WriteLine("no text child node?");
				}
			}
			return wf;
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
