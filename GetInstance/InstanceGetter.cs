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
			const string period = "2022";
			string appFolder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			XDocument responseBody = await downloader.Download(param);

			Console.WriteLine(responseBody.ToString());
			IEnumerable<XDocument> instances = ExtractInstance(responseBody);

			Console.WriteLine("3. Processing CDR Data: ");

			string instanceFileName = Path.Combine(appFolder, period + "-Instance.txt");
			if (instances == null || instances.Count() == 0)
			{
				string interpretedErrorMessage = PrepareMsg(responseBody.ToString());
				AskPreviousFile(instanceFileName);
			}
			else
			{
				int instanceFilenameSuffix = 0;
				foreach (XDocument instanceXml in instances)
				{
					XPathNodeIterator ccRecordsIterator = GetCCIterator(instanceXml);
					List<WriteFormat> wf = DeserializeRecords(ccRecordsIterator);
					instanceFileName = Path.Combine(appFolder, period + "-Instance-" + instanceFilenameSuffix++.ToString() + ".txt");
					writer.Write(records: wf, instanceFileName);
					Console.WriteLine("Completed");
					Console.WriteLine("CDR Live Update: Successful");
					Console.WriteLine("Prior quarter history data has been downloaded successfully.");
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
			IEnumerable<XDocument> ExtractInstance(XDocument responseBody)
			{
				List<XDocument> instanceDocs = new();
				if (responseBody?.Element(XName.Get("GetInstanceDataResponse", "http://ffiec.gov/cdr/services/"))
					?.Element(XName.Get("GetInstanceDataResult", "http://ffiec.gov/cdr/services/")) is XElement cdr)
				{
					string cdrText = cdr.Value.Replace("&lt;", "<").Replace("&gt;", ">");
					//remove the declaration for xbrl document. another option would be processing xbrl under <InstanceDocument> as string and convert it to XDocument separately
					cdrText = cdrText.Replace(@"<?xml version=""1.0"" encoding=""utf-8""?>", string.Empty);
					using (StringReader sr = new(cdrText))
						cdr = XElement.Load(new StringReader(cdrText));
					if (cdr.Name == XName.Get("CdrServiceGetInstanceData", "http://Cdr.Business.Workflow.Schemas.CdrServiceGetInstanceData") &&
						cdr.Element(XName.Get("Outputs")) is XElement outputs)
					{
						XElement returnCodeElement = outputs.Element("ReturnCode");
						XElement errorCodeElement = outputs.Element("ErrorCode");
						string codeText = (returnCodeElement ?? errorCodeElement)?.Value;
						bool IsSuccessfulGetInstance = int.TryParse(codeText, out int code) && code == 0;
						try
						{
							if (IsSuccessfulGetInstance && outputs.Element(XName.Get("InstanceDocuments"))?.Elements("InstanceDocument") is IEnumerable<XElement> instances)
							{
								foreach (XElement instance in instances)
									if (instance.Element(XName.Get("xbrl", "http://www.xbrl.org/2003/instance")) is XElement xbrl)
										instanceDocs.Add(new XDocument(xbrl));
							}
							else
							{
								XElement returnMessageElement = outputs.Element("ReturnMessage");
								XElement errorMessageElement = outputs.Element("ErrorMessage");
								string message = (returnMessageElement ?? errorMessageElement)?.Value;
								Console.WriteLine("Code: " + code);
								Console.WriteLine("Message: " + message);
							}
						}
						catch (Exception ex) when (ex is InvalidCastException || ex is FormatException)
						{
							Console.WriteLine("Unable to parse ErrorCode: " + code);
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
						}
					}
				}
				return instanceDocs;
			}
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