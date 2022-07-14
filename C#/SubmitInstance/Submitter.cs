using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace SubmitInstance
{
	public class Submitter
	{
		IInstanceReader reader;
		IInstancePoster poster;
		ISettings settings;
		ISubmitRequestBuilder builder;

		public Submitter(IInstanceReader reader, IInstancePoster poster, ISettings settings, ISubmitRequestBuilder builder)
		{
			this.reader = reader;
			this.poster = poster;
			this.settings = settings;
			this.builder = builder;
		}

		public async Task<bool> Submit(SubmitParam param)
		{
			Console.WriteLine("Submitting Call Report to the CDR");
			Console.WriteLine("1. Logging on to CDR");
			XDocument responseBody = null;
			try
			{
				string url = param.Prod ? settings.Url : settings.UrlT;
				string action = param.Prod ? settings.SubmitAction : settings.TestSubmitAction;

				Console.WriteLine("2. Submitting Call Report: ");
				Console.WriteLine(url);
				Console.WriteLine(action);

				string fileContents = reader.Read(param.Filename);
				if(string.IsNullOrWhiteSpace(fileContents))
				{
					return false;
				}
				XDocument submitBody = builder.Build(param, settings.NS, fileContents);

				responseBody = await poster.Post(url, action, submitBody);

				Console.WriteLine(responseBody.ToString());

				var response = new Cdr().ExtractSubmitResponse(responseBody, param.Prod);
				bool IsSuccessfulGetInstance = response.ReturnCode == 0;
				if (IsSuccessfulGetInstance)
				{
					return true;
				}
				else
				{
					Console.WriteLine("Code: " + response.ReturnCode);
					Console.WriteLine("Message: " + response.ReturnMessage);
					return false;
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
						"Unable to connect. Some firewalls require altering permissions to allow EasyCall Report to communicate with the Central Data Repository.  Your information technology department should be made aware that this communication uses HTTPS via port 443." +
						"Also, the FFIEC CDR system now only supports TLS 1.2;  please insure your operating system supports said.";
				Console.WriteLine(PrepareMsg(errmsg));
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

	[XmlRoot("SubmitInstanceData", Namespace = "http://ffiec.gov/cdr/services/")]
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