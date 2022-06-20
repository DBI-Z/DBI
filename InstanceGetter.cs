using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace GetInstance
{
	internal class InstanceGetter
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
			const int numberOfPeriods = 6;
			const string contextRef = "CI_2631314_2005-03-31"; //TODO:input?
			const string period = "2022"; //TODO: input?
			string appFolder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			string instanceFileName = Path.Combine(appFolder, period + "-Instance.txt");
			XDocument responseBody = await downloader.Download(param);
			Console.WriteLine(responseBody.ToString());
			List<XDocument> instances = ExtractInstance(responseBody);

			Console.WriteLine("3. Processing CDR Data: ");

			if (instances == null || instances.Count == 0)
			{
				AskPreviousFile(instanceFileName);
			}
			else
			{
				foreach (XDocument instanceXml in instances)
				{
					XPathNodeIterator ccRecordsIterator = GetCCIterator(instanceXml);
					List<WriteFormat> wf = DeserializeRecords(ccRecordsIterator);
					writer.Write(records: wf, instanceFileName);
					Console.WriteLine("Completed");
					Console.WriteLine("CDR Live Update: Successful");
					Console.WriteLine("Prior quarter history data has been downloaded successfully.");
				}
			}
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
		List<XDocument> ExtractInstance(XDocument responseBody)
		{
			if (responseBody == null)
				return null;

			//XDocument mock;
			//using (var sr = new StringReader(TestInputGetInstanceResponse.Test4))
			//mock = XDocument.Load(sr);
			//XPathNavigator nav = mock.CreateNavigator();

			XPathNavigator responseBodyNav = responseBody.CreateNavigator();
			XmlNamespaceManager m = new(responseBodyNav.NameTable);
			//m.AddNamespace(string.Empty, "http://Cdr.Business.Workflow.Schemas.CdrServiceGetInstanceData");
			m.AddNamespace("def", "http://ffiec.gov/cdr/services/");

			//XPathNodeIterator it = nav.Evaluate("/", m) as XPathNodeIterator;
			//XPathNodeIterator it = nav.Evaluate("/def:GetInstanceDataResponse",m ) as XPathNodeIterator;

			XPathNavigator getInstanceDataResult = responseBodyNav.SelectSingleNode("/def:GetInstanceDataResponse/def:GetInstanceDataResult", m);

			if (getInstanceDataResult == null)
				return null;

			XDocument cdrResult = XDocument.Load(new StringReader(getInstanceDataResult.Value));
			XPathNavigator cdrNav = cdrResult.CreateNavigator();
			m = new(cdrNav.NameTable);
			m.AddNamespace("def", "http://Cdr.Business.Workflow.Schemas.CdrServiceGetInstanceData");
			//using (var sr = new StringReader(TestInputGetInstanceResponse.Test4))
			//var aaa = instance.SelectSingleNode("/*", m);
			if (cdrNav.SelectSingleNode("/def:CdrServiceGetInstanceData/Outputs", m) is not XPathNavigator cdrOutputs)
				return null;

			try
			{
				int? success = 0;
				int? errorCode = cdrOutputs.SelectSingleNode("ErrorCode", m)?.ValueAsInt;
				if (errorCode != success)
				{
					string errorMessage = cdrOutputs.SelectSingleNode("ErrorMessage", m)?.Value;
					Console.WriteLine("ErrorCode: " + errorCode);
					Console.WriteLine("ErrorMessage: " + errorMessage);
					return null;
				}
				else
				{
					List<XDocument> results = new List<XDocument>();
					XPathNodeIterator instances = cdrOutputs.Select("InstanceDocuments/*");
					foreach (var instance in instances)
					{
						XDocument doc = new XDocument();
					}
					return results;
				}
			}
			catch (Exception ex)
			{
				if (ex is InvalidCastException || ex is FormatException)
				{
					Console.WriteLine(ex.Message);
					Console.WriteLine("Unable to parse ErrorCode: " + cdrNav.SelectSingleNode("/def:CdrServiceGetInstanceData/Outputs/ErrorCode", m)?.Value);
				}
				return null;
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

public class Inputs
{
	[XmlElement("RSSD")]
	public int RSSD;
	[XmlElement("ReportingPeriodEndDate")]
	public DateTime ReportingPeriodEndDate { get; set; }
	[XmlElement("NumberOfPriorPeriods")]
	public int NumberOfPriorPeriods { get; set; }
}

public class Outputs
{
	[XmlElement("ErrorCode")]
	public int ErrorCode { get; set; }
	[XmlElement("ErrorMessage")]
	public string ErrorMessage { get; set; }
}