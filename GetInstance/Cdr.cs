using System.Xml.Linq;

namespace GetInstance
{
	public class Cdr
	{
		public GetInstanceResponse ExtractGetResponse(XDocument responseBody)
		{
			if (responseBody?.Element(XName.Get("GetInstanceDataResponse", "http://ffiec.gov/cdr/services/"))
				?.Element(XName.Get("GetInstanceDataResult", "http://ffiec.gov/cdr/services/")) is XElement getInstanceDataResult)
			{
				//string cdrText= String.Empty;
				//if (getInstanceDataResult.Value.Contains("&lt;"))
				{
					XElement cdr = getInstanceDataResult.Element(XName.Get("CdrServiceGetInstanceData", "http://Cdr.Business.Workflow.Schemas.CdrServiceGetInstanceData"));

					if (cdr == null)
					{
						//remove the declaration for xbrl document. another option would be processing xbrl under <InstanceDocument> as string and convert it to XDocument separately
						string cdrText = getInstanceDataResult.Value.Replace("&lt;", "<").Replace("&gt;", ">").Replace(@"<?xml version=""1.0"" encoding=""utf-8""?>", string.Empty);
						using (StringReader sr = new(cdrText))
							cdr = XElement.Load(new StringReader(cdrText));
					}

					if (cdr?.Name == XName.Get("CdrServiceGetInstanceData", "http://Cdr.Business.Workflow.Schemas.CdrServiceGetInstanceData") &&
						cdr.Element(XName.Get("Outputs")) is XElement outputs)
					{
						XElement returnCodeElement = outputs.Element("ReturnCode");
						XElement errorCodeElement = outputs.Element("ErrorCode");
						string codeText = (returnCodeElement ?? errorCodeElement)?.Value;
						XElement returnMessageElement = outputs.Element("ReturnMessage");
						XElement errorMessageElement = outputs.Element("ErrorMessage");
						string message = (returnMessageElement ?? errorMessageElement)?.Value;
						Console.WriteLine("Code: " + codeText);
						Console.WriteLine("Message: " + message);
						int? retcode = int.TryParse(codeText, out int code) ? code : null;
						XElement instanceDocuments = outputs.Element(XName.Get("InstanceDocuments"));
						return new GetInstanceResponse
						{
							Code = code,
							Message = message,
							InstanceDocuments = new XDocument(instanceDocuments)
						};
					}
				}
			}
			return null;
		}
	}
}
