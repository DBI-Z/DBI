Imports System.IO

Public Class Cdr
	Public Function ExtractSubmitResponse(ByVal responseBody As XDocument, ByVal prod As Boolean) As (ReturnCode As Integer?, ReturnMessage As String)
		Dim submitResponseElement As String = If(prod, Settings.SubmitResponseProd, Settings.SubmitResponseTest)
		Dim submitResultElement As String = If(prod, Settings.SubmitResultProd, Settings.SubmitResultTest)
		Dim code As Integer = Nothing, submitResult As XElement = Nothing, outputs As XElement = Nothing

		If CSharpImpl.__Assign(submitResult, TryCast(responseBody?.Element(XName.[Get](submitResponseElement, "http://ffiec.gov/cdr/services/"))?.Element(XName.[Get](submitResultElement, "http://ffiec.gov/cdr/services/")), XElement)) IsNot Nothing Then
			Dim cdr As XElement = submitResult.Element(XName.[Get]("CdrServiceSubmitInstanceData", "http://Cdr.Business.Workflow.Schemas.CdrServiceSubmitInstanceData"))

			If cdr Is Nothing Then
				Dim cdrText As String = submitResult.Value.Replace("&lt;", "<").Replace("&gt;", ">").Replace("<?xml version=""1.0"" encoding=""utf-8""?>", String.Empty)

				Dim sr As StringReader = New StringReader(cdrText)
				cdr = XElement.Load(New StringReader(cdrText))

			End If

			Dim cdrNamespace As String = "http://Cdr.Business.Workflow.Schemas.CdrServiceSubmitInstanceData"

			If cdr?.Name = XName.[Get]("CdrServiceSubmitInstanceData", cdrNamespace) AndAlso CSharpImpl.__Assign(outputs, TryCast(cdr.Element("Outputs"), XElement)) IsNot Nothing Then
				Dim returnCodeElement As XElement = outputs.Element("ReturnCode")
				Dim errorCodeElement As XElement = outputs.Element("ErrorCode")
				Dim codeText As String = (If(returnCodeElement, errorCodeElement))?.Value
				Dim returnMessageElement As XElement = outputs.Element("ReturnMessage")
				Dim errorMessageElement As XElement = outputs.Element("ErrorMessage")
				Dim message As String = (If(returnMessageElement, errorMessageElement))?.Value
				Integer.TryParse(codeText, code)
				Return (code, message)
			End If
		End If

		Return (Nothing, Nothing)
	End Function

	Private Class CSharpImpl
		<Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
		Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
			target = value
			Return value
		End Function
	End Class
End Class