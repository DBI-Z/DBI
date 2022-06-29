using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace SubmitInstance
{
	internal class Submitter
	{
		IInstanceReader reader;
		IInstancePoster poster;

		public Submitter(IInstanceReader reader, IInstancePoster poster)
		{
			this.reader = reader;
			this.poster = poster;
		}

		//using a login info, submit the contents of filename denoted at InstanceDataFileText.Text
		//either test or production server can be targeted
		const string serviceUrlLive = "https://cdr.ffiec.gov/cdr/Services/CdrService.asmx?WSDL";
		const string serviceUrlTest = "https://c1-test-cdr-ffiec.com/FFIEC/services/CdrService.asmx?WSDL";
		const string submitActionLive = "http://ffiec.gov/cdr/services/SubmitInstanceData";
		const string submitActionTest = "http://ffiec.gov/cdr/services/SubmitTestInstanceData";
 
		public async Task<bool> Submit(SubmitParam param)
		{
			Console.WriteLine("Submitting Call Report to the CDR");
			Console.WriteLine("1. Logging on to CDR");
			XPathNavigator cdrOutputs = null;
			XmlNamespaceManager m = null;
			XDocument responseBody = null;
			try
			{
				string fileContents = reader.Read(param.Filename);
				string instance = fileContents.Replace("<", "&lt;").Replace(">", "&gt;");
				SubmitInstanceDataRequest request = new()
				{
					InstanceData = instance,
					Password = param.Password,
					UserName = param.Username
				};
				if(!param.Prod)
				{
					Console.WriteLine("test is not yet supported. please use prod");
					return false;
				}

				string url = param.Prod ? serviceUrlLive : serviceUrlTest;
				string action = param.Prod ? submitActionLive : submitActionTest;
				Console.WriteLine("2. Submitting Call Report: ");
				Console.WriteLine(url);
				Console.WriteLine(action);

				responseBody = await poster.Post(url, action, request);

				Console.WriteLine(responseBody.ToString());

				XPathNavigator responseBodyNav = responseBody.CreateNavigator();
				m = new(responseBodyNav.NameTable);
				m.AddNamespace("def", "http://ffiec.gov/cdr/services/");
				XPathNavigator submitInstanceDataResult = responseBodyNav.SelectSingleNode("/def:SubmitInstanceDataResponse/def:SubmitInstanceDataResult", m);

				if (submitInstanceDataResult == null)
					return false;

				XDocument cdrResult = XDocument.Load(new StringReader(submitInstanceDataResult.Value));
				XPathNavigator cdrNav = cdrResult.CreateNavigator();
				m = new(cdrNav.NameTable);
				m.AddNamespace("def", "http://Cdr.Business.Workflow.Schemas.CdrServiceSubmitInstanceData");
				cdrOutputs = cdrNav.SelectSingleNode("/def:CdrServiceSubmitInstanceData/Outputs", m);
				if (cdrOutputs == null)
					return false;

				int? success = 0;
				int? errorCode = cdrOutputs.SelectSingleNode("ErrorCode", m)?.ValueAsInt;
				if (errorCode != success)
				{
					string errorMessage = cdrOutputs.SelectSingleNode("ErrorMessage", m)?.Value;
					Console.WriteLine("ErrorCode: " + errorCode);
					Console.WriteLine("ErrorMessage: " + errorMessage);
					return false;
				}
				else
				{
					return true;
				}
			}
			catch (IOException ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine($"Error while reading file {param.Filename}");
				return false;
			}
			catch (HttpRequestException ex)
			{
				Console.WriteLine(ex.Message);
				string errmsg =
						"Unable to connect.  Some firewalls require altering permissions to allow EasyCall Report to communicate with the Central Data Repository.  Your information technology department should be made aware that this communication uses HTTPS via port 443." +
						"Also, the FFIEC CDR system now only supports TLS 1.2;  please insure your operating system supports said.";
				Console.WriteLine(PrepareMsg(errmsg));
				return false;
			}
			catch (Exception ex) when (ex is InvalidCastException || ex is FormatException)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine("Unable to parse ErrorCode: " + cdrOutputs?.SelectSingleNode("ErrorCode", m)?.Value);
				return false;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				string interpretedErrorMessage = PrepareMsg(responseBody?.ToString() ?? string.Empty);
				Console.WriteLine(interpretedErrorMessage);
				return false;
			} 
		}
			string PrepareMsg(string subText)
			{
				const int defaultErrorCode = 6;
				(string phrase, int code)[] errorPhrases = new[]
				{
						("not authorized",1 ),
						("not match",2 ),
						("Access denied",3 ),
						("valid XML",4 ),
						("ID_RSSD",5 )
				};

				StringBuilder sb = new();

				int errorCode = defaultErrorCode;
				for (int i = 0; i < errorPhrases.Length; i++)
					if (subText.IndexOf(errorPhrases[i].phrase) > 0)
						errorCode = i;

				sb.AppendLine("A problem was encountered while attempting to submit your Call Report to the CDR.");
				switch (errorCode)
				{
					case 1:
						sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.");
						break;
					case 4:
						sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.");
						break;
					case 5:
					case defaultErrorCode:
						sb.AppendJoin("Error", errorCode, subText);
						break;
					case 2:
						sb.AppendLine("Invalid User Name or Password, or User is locked.");
						sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.");
						break;
					case 3:
						sb.AppendLine("Invalid User Name or Password, User is locked, or mandatory training must be completed.");
						sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.");
						break;
				}

				sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.");
				return sb.ToString();
			}
	}

	[XmlRoot("SubmitInstanceData",  Namespace = "http://ffiec.gov/cdr/services/")]
	public class SubmitInstanceDataRequest
	{
		[XmlElement("userName")]
		public string UserName { get; set; }
		[XmlElement("password")]
		public string Password { get; set; }
		[XmlElement("instanceData")]
		public string InstanceData { get; set; }
	} 
}