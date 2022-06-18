using System.Diagnostics;
using System.Xml;
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
			string instanceXmlText = await downloader.Download(param);
			Console.WriteLine("3. Processing CDR Data: ");

			if (string.IsNullOrWhiteSpace(instanceXmlText))
			{
				AskPreviousFile();
			}
			else
			{
				XmlDocument instanceXml = new XmlDocument();
				instanceXml.LoadXml(instanceXmlText);
				XPathNodeIterator ccRecordsIterator = GetCCIterator(instanceXml);
				if (CheckErrorCode(instanceXml))
				{
					List<WriteFormat> wf = DeserializeRecords(ccRecordsIterator);
					writer.Write(records: wf, instanceFileName);
					Console.WriteLine("Completed");
					Console.WriteLine("CDR Live Update: Successful");
					Console.WriteLine("Prior quarter history data has been downloaded successfully.");
				}
			}
		}

		XPathNodeIterator GetCCIterator(XmlDocument doc)
		{
			XPathNavigator n = doc.CreateNavigator();
			XmlNamespaceManager m = new XmlNamespaceManager(n.NameTable);
			m.AddNamespace("cc", "http://www.ffiec.gov/xbrl/call/concepts");
			return n.Evaluate("//cc:*", m) as XPathNodeIterator;
		}

		bool CheckErrorCode(XmlDocument root)
		{
			var nav = root.CreateNavigator();
			var it = nav.Evaluate("/CdrServicecGetInstanceData/Outputs/ErrorCode") as XPathNodeIterator;

			if (it == null || !it.MoveNext())
			{
				Console.WriteLine("Live CDR Update Error B: ");
				return false;
			}
			else
			{
				if (it.Current.ValueAsInt == 0)
				{
					return true;
				}
				else
				{
					it = nav.Evaluate("/CdrServiceGetInstanceData/Outputs/ErrorMessage") as XPathNodeIterator;
					if (it == null || !it.MoveNext())
					{
						Console.WriteLine("Live CDR Update Error B: ");
						return null;
					}
					else
					{

					}
				}
			}

		}
		void AskPreviousFile()
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

//[XmlRoot("GetInstanceDataResponse", Namespace = "http://ffiec.gov/cdr/services/")]
//public class GetInstanceDataResponse
//{
//	//[XmlElement("Inputs")]
//	//public Inputs Inputs { get; set; }

//	//[XmlElement("Outputs")]
//	//public Outputs? Outputs { get; set; }

//	[XmlElement("GetInstanceDataResult")]
//	public string GetInstanceDataResult { get; set; }
//}

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