using Xunit;
using System.Xml.Linq;

namespace SubmitInstance.Test
{
	public class SubmitterTests
	{
		[Fact]
		public void SubmitRequest_BuildBody_RequestBodyIsTest()
		{
			//Arrange
			RequestBuilder rb = new();
			SubmitParam submitParam = new() { Prod = false };

			//Act
			XDocument body = rb.Build(submitParam, string.Empty, string.Empty);

			//Assert
			string bodyProse = body.ToString();
			Assert.Contains(Settings.SubmitRequestTest, bodyProse);
		}

		[Fact]
		public void SubmitRequest_BuildBody_RequestBodyIsProd()
		{
			//Arrange
			RequestBuilder rb = new();
			SubmitParam submitParam = new() { Prod = true };

			//Act
			XDocument body = rb.Build(submitParam, string.Empty, string.Empty);

			//Assert
			string bodyProse = body.ToString();
			Assert.Contains(Settings.SubmitRequestProd, bodyProse);
		}

		[Fact]
		public void SubmitRequest_BuildBody_UserPassIncluded()
		{
			//Arrange
			const string username = "user1234";
			const string password = "pass1234";
			RequestBuilder rb = new();
			SubmitParam submitParam = new() { Prod = true, Username = username, Password = password };

			//Act
			XDocument body = rb.Build(submitParam, string.Empty, string.Empty);

			//Assert
			string bodyProse = body.ToString();
			Assert.Contains(username, bodyProse);
			Assert.Contains(password, bodyProse);
		}

		[Fact]
		public void SubmitRequest_BuildBody_ContentIncluded()
		{
			//Arrange
			const string checkValue = "4a4b-5188-aafd-dd31";
			const string document = $"<xbrl>{checkValue}</xbrl>";
			RequestBuilder rb = new();
			SubmitParam submitParam = new() { Prod = true };

			//Act
			XDocument body = rb.Build(submitParam, string.Empty, document);

			//Assert
			string bodyProse = body.ToString();
			Assert.Contains(checkValue, bodyProse);
		}

		[Fact]
		public void Submit_Response_ReadErrorCode()
		{
			//Arrange
			string responseTemplate = @"<SubmitTestInstanceDataResponse xmlns=""http://ffiec.gov/cdr/services/"">
																																					<SubmitTestInstanceDataResult>
																																						<CdrServiceSubmitInstanceData xmlns=""http://Cdr.Business.Workflow.Schemas.CdrServiceSubmitInstanceData"">
																																							<Outputs xmlns="""">
																																								<ErrorCode>100</ErrorCode>
																																								<ErrorMessage>ErrorMessage100</ErrorMessage>
																																							</Outputs>
																																						</CdrServiceSubmitInstanceData>
																																					</SubmitTestInstanceDataResult>
																																			</SubmitTestInstanceDataResponse>";

			XDocument responseXml;
			using (var responseStream = new StringReader(responseTemplate))
				responseXml = XDocument.Load(responseStream);

			//Act 
			var response = new Cdr().ExtractSubmitResponse(responseXml, false);

			//Assert
			Assert.Equal(100, response.ReturnCode);
			Assert.Equal("ErrorMessage100", response.ReturnMessage);
		}

		[Fact]
		public void Submit_Response_ReadReturnCode()
		{
			//Arrange
			string responseTemplate = @"<SubmitTestInstanceDataResponse xmlns=""http://ffiec.gov/cdr/services/"">
																																					<SubmitTestInstanceDataResult>
																																						<CdrServiceSubmitInstanceData xmlns=""http://Cdr.Business.Workflow.Schemas.CdrServiceSubmitInstanceData"">
																																							<Outputs xmlns="""">
																																								<ReturnCode>100</ReturnCode>
																																								<ReturnMessage>ReturnMessage100</ReturnMessage>
																																							</Outputs>
																																						</CdrServiceSubmitInstanceData>
																																					</SubmitTestInstanceDataResult>
																																			</SubmitTestInstanceDataResponse>";

			XDocument responseXml;
			using (var responseStream = new StringReader(responseTemplate))
				responseXml = XDocument.Load(responseStream);

			//Act 
			var response = new Cdr().ExtractSubmitResponse(responseXml, false);

			//Assert
			Assert.Equal(100, response.ReturnCode);
			Assert.Equal("ReturnMessage100", response.ReturnMessage);
		}

		[Fact]
		public void Submit_Response_NullReturnCode()
		{
			//Arrange
			string responseTemplate = @"<?xml version=""1.0"" encoding=""utf-8""?>
																															<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
																																	<soap:Body>
																																			<SubmitTestInstanceDataResponse xmlns=""http://ffiec.gov/cdr/services/"">
																																					<SubmitTestInstanceDataResult>string</SubmitTestInstanceDataResult>
																																			</SubmitTestInstanceDataResponse>
																																	</soap:Body>
																															</soap:Envelope>";

			XDocument responseXml;
			using (var responseStream = new StringReader(responseTemplate))
				responseXml = XDocument.Load(responseStream);

			//Act 
			var response = new Cdr().ExtractSubmitResponse(responseXml, false);

			//Assert
			Assert.Null(response.ReturnCode);
			Assert.Null(response.ReturnMessage);
		}

		[Fact]
		public void Submit_Settings_ParsedCorrectly()
		{
			//Arrange
			string sampleContents = @"GetAction = http://ffiec.gov/cdr/services/GetInstanceData
																													URL = https://c1-test-cdr-ffiec.com/FFIEC/services/CdrService.asmx?WSDL
																													URLT = https://c1-test-cdr-ffiec.com/FFIEC/services/CdrService.asmx?WSDL
																													NS = http://ffiec.gov/cdr/services/
																													SubmitAction = http://ffiec.gov/cdr/services/SubmitInstanceData
																													TestSubmitAction = http://ffiec.gov/cdr/services/SubmitTestInstanceData";
			MemoryStream ms = new(sampleContents.Length);
			StreamWriter sw = new(ms);
			sw.Write(sampleContents);
			sw.Flush();
			ms.Seek(0, SeekOrigin.Begin);

			//Act
			Settings settings = new();
			settings.Load(ms);

			//Cleanup
			sw.Dispose();
			ms.Dispose();

			//Assert
			Assert.Equal("http://ffiec.gov/cdr/services/GetInstanceData", settings.GetAction);
			Assert.Equal("https://c1-test-cdr-ffiec.com/FFIEC/services/CdrService.asmx?WSDL", settings.Url);
			Assert.Equal("https://c1-test-cdr-ffiec.com/FFIEC/services/CdrService.asmx?WSDL", settings.UrlT);
			Assert.Equal("http://ffiec.gov/cdr/services/", settings.NS);
			Assert.Equal("http://ffiec.gov/cdr/services/SubmitInstanceData", settings.SubmitAction);
			Assert.Equal("http://ffiec.gov/cdr/services/SubmitTestInstanceData", settings.TestSubmitAction);
		}
	}
}