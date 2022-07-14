using System.Xml.Linq;

namespace SubmitInstance
{
	public class Cdr
	{
		public (int? ReturnCode, string? ReturnMessage) ExtractSubmitResponse(XDocument responseBody, bool prod)
		{
			string submitResponseElement = prod ? Settings.SubmitResponseProd : Settings.SubmitResponseTest;
			string submitResultElement = prod ? Settings.SubmitResultProd : Settings.SubmitResultTest;

			if (responseBody?.Element(XName.Get(submitResponseElement, "http://ffiec.gov/cdr/services/"))
				?.Element(XName.Get(submitResultElement, "http://ffiec.gov/cdr/services/")) is XElement submitResult)
			{ 
				XElement cdr = submitResult.Element(XName.Get("CdrServiceSubmitInstanceData", "http://Cdr.Business.Workflow.Schemas.CdrServiceSubmitInstanceData"));
				if (cdr == null)
				{
					string cdrText = submitResult.Value.Replace("&lt;", "<").Replace("&gt;", ">").Replace(@"<?xml version=""1.0"" encoding=""utf-8""?>", string.Empty);
					using (StringReader sr = new(cdrText))
						cdr = XElement.Load(new StringReader(cdrText));
				}
				string cdrNamespace = "http://Cdr.Business.Workflow.Schemas.CdrServiceSubmitInstanceData";

				if (cdr?.Name == XName.Get("CdrServiceSubmitInstanceData", cdrNamespace) &&
	cdr.Element("Outputs") is XElement outputs)
				{
					XElement returnCodeElement = outputs.Element("ReturnCode");
					XElement errorCodeElement = outputs.Element("ErrorCode");
					string codeText = (returnCodeElement ?? errorCodeElement)?.Value;
					XElement returnMessageElement = outputs.Element("ReturnMessage");
					XElement errorMessageElement = outputs.Element("ErrorMessage");
					string message = (returnMessageElement ?? errorMessageElement)?.Value;
					int.TryParse(codeText, out int code);
					return (code, message);
				}
			}
			return (null, null);
		}
	}
}