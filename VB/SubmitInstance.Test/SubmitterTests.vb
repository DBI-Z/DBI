Imports System.IO
Imports Xunit

Namespace SubmitInstance.Test
	Public Class SubmitterTests
		<Fact>
		Public Sub SubmitRequest_BuildBody_RequestBodyIsTest()
			Dim rb As RequestBuilder = New RequestBuilder()
			Dim submitParam As SubmitParam = New SubmitParam()
			submitParam.Prod = False
			Dim body As XDocument = rb.Build(submitParam, String.Empty, String.Empty)
			Dim bodyProse As String = body.ToString()
			Assert.Contains(Settings.SubmitRequestTest, bodyProse)
		End Sub

		<Fact>
		Public Sub SubmitRequest_BuildBody_RequestBodyIsProd()
			Dim rb As RequestBuilder = New RequestBuilder()
			Dim submitParam As SubmitParam = New SubmitParam()
			submitParam.Prod = True
			Dim body As XDocument = rb.Build(submitParam, String.Empty, String.Empty)
			Dim bodyProse As String = body.ToString()
			Assert.Contains(Settings.SubmitRequestProd, bodyProse)
		End Sub

		<Fact>
		Public Sub SubmitRequest_BuildBody_UserPassIncluded()
			Const username As String = "user1234"
			Const password As String = "pass1234"
			Dim rb As RequestBuilder = New RequestBuilder
			Dim submitParam As SubmitParam = New SubmitParam
			submitParam.Prod = True
			submitParam.Username = username
			submitParam.Password = password
			Dim body As XDocument = rb.Build(submitParam, String.Empty, String.Empty)
			Dim bodyProse As String = body.ToString()
			Assert.Contains(username, bodyProse)
			Assert.Contains(password, bodyProse)
		End Sub

		<Fact>
		Public Sub SubmitRequest_BuildBody_ContentIncluded()
			Const checkValue As String = "4a4b-5188-aafd-dd31"
			Const document As String = "<xbrl>" + checkValue + "</xbrl>"
			Dim rb As RequestBuilder = New RequestBuilder
			Dim submitParam As SubmitParam = New SubmitParam
			submitParam.Prod = True
			Dim body As XDocument = rb.Build(submitParam, String.Empty, document)
			Dim bodyProse As String = body.ToString()
			Assert.Contains(checkValue, bodyProse)
		End Sub

		<Fact>
		Public Sub Submit_Response_ReadErrorCode()
			Dim responseTemplate As String = "<SubmitTestInstanceDataResponse xmlns=""http://ffiec.gov/cdr/services/"">
																																					<SubmitTestInstanceDataResult>
																																						<CdrServiceSubmitInstanceData xmlns=""http://Cdr.Business.Workflow.Schemas.CdrServiceSubmitInstanceData"">
																																							<Outputs xmlns="""">
																																								<ErrorCode>100</ErrorCode>
																																								<ErrorMessage>ErrorMessage100</ErrorMessage>
																																							</Outputs>
																																						</CdrServiceSubmitInstanceData>
																																					</SubmitTestInstanceDataResult>
																																			</SubmitTestInstanceDataResponse>"
			Dim responseXml As XDocument

			Using responseStream = New StringReader(responseTemplate)
				responseXml = XDocument.Load(responseStream)
			End Using

			Dim response = New Cdr().ExtractSubmitResponse(responseXml, False)
			Assert.Equal(100, response.ReturnCode)
			Assert.Equal("ErrorMessage100", response.ReturnMessage)
		End Sub

		<Fact>
		Public Sub Submit_Response_ReadReturnCode()
			Dim responseTemplate As String = "<SubmitTestInstanceDataResponse xmlns=""http://ffiec.gov/cdr/services/"">
																																					<SubmitTestInstanceDataResult>
																																						<CdrServiceSubmitInstanceData xmlns=""http://Cdr.Business.Workflow.Schemas.CdrServiceSubmitInstanceData"">
																																							<Outputs xmlns="""">
																																								<ReturnCode>100</ReturnCode>
																																								<ReturnMessage>ReturnMessage100</ReturnMessage>
																																							</Outputs>
																																						</CdrServiceSubmitInstanceData>
																																					</SubmitTestInstanceDataResult>
																																			</SubmitTestInstanceDataResponse>"
			Dim responseXml As XDocument

			Using responseStream = New StringReader(responseTemplate)
				responseXml = XDocument.Load(responseStream)
			End Using

			Dim response = New Cdr().ExtractSubmitResponse(responseXml, False)
			Assert.Equal(100, response.ReturnCode)
			Assert.Equal("ReturnMessage100", response.ReturnMessage)
		End Sub

		<Fact>
		Public Sub Submit_Response_NullReturnCode()
			Dim responseTemplate As String = "<?xml version=""1.0"" encoding=""utf-8""?>
																															<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
																																	<soap:Body>
																																			<SubmitTestInstanceDataResponse xmlns=""http://ffiec.gov/cdr/services/"">
																																					<SubmitTestInstanceDataResult>string</SubmitTestInstanceDataResult>
																																			</SubmitTestInstanceDataResponse>
																																	</soap:Body>
																															</soap:Envelope>"
			Dim responseXml As XDocument

			Using responseStream = New StringReader(responseTemplate)
				responseXml = XDocument.Load(responseStream)
			End Using

			Dim response = New Cdr().ExtractSubmitResponse(responseXml, False)
			Assert.Null(response.ReturnCode)
			Assert.Null(response.ReturnMessage)
		End Sub

		<Fact>
		Public Sub Submit_Settings_ParsedCorrectly()
			Dim sampleContents As String = "GetAction = http://ffiec.gov/cdr/services/GetInstanceData
																													URL = https://c1-test-cdr-ffiec.com/FFIEC/services/CdrService.asmx?WSDL
																													URLT = https://c1-test-cdr-ffiec.com/FFIEC/services/CdrService.asmx?WSDL
																													NS = http://ffiec.gov/cdr/services/
																													SubmitAction = http://ffiec.gov/cdr/services/SubmitInstanceData
																													TestSubmitAction = http://ffiec.gov/cdr/services/SubmitTestInstanceData"
Dim ms As MemoryStream = New MemoryStream (sampleContents .Length )
Dim sw As StreamWriter = New StreamWriter(ms)
			sw.Write(sampleContents)
			sw.Flush()
			ms.Seek(0, SeekOrigin.Begin)
Dim settings As  Settings = New Settings
			settings.Load(ms)
			sw.Dispose()
			ms.Dispose()
			Assert.Equal("http://ffiec.gov/cdr/services/GetInstanceData", Settings.GetAction)
			Assert.Equal("https://c1-test-cdr-ffiec.com/FFIEC/services/CdrService.asmx?WSDL", Settings.Url)
			Assert.Equal("https://c1-test-cdr-ffiec.com/FFIEC/services/CdrService.asmx?WSDL", Settings.UrlT)
			Assert.Equal("http://ffiec.gov/cdr/services/", Settings.NS)
			Assert.Equal("http://ffiec.gov/cdr/services/SubmitInstanceData", Settings.SubmitAction)
			Assert.Equal("http://ffiec.gov/cdr/services/SubmitTestInstanceData", Settings.TestSubmitAction)
		End Sub
	End Class
End Namespace