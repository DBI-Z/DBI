Imports System.IO
Imports System.Xml.Linq

Namespace GetInstance
	Public Class Cdr
		Private displayer As IDisplayer

		Public Sub New(ByVal displayer As IDisplayer)
			Me.displayer = displayer
		End Sub

		Public Function ExtractGetResponse(ByVal responseBody As XDocument) As GetInstanceResponse
			Dim code As Integer = Nothing, getInstanceDataResult As XElement = Nothing, outputs As XElement = Nothing

			If CSharpImpl.__Assign(getInstanceDataResult, TryCast(responseBody?.Element(XName.[Get]("GetInstanceDataResponse", "http://ffiec.gov/cdr/services/"))?.Element(XName.[Get]("GetInstanceDataResult", "http://ffiec.gov/cdr/services/")), XElement)) IsNot Nothing Then

				If True Then
					Dim cdr As XElement = getInstanceDataResult.Element(XName.[Get]("CdrServiceGetInstanceData", "http://Cdr.Business.Workflow.Schemas.CdrServiceGetInstanceData"))

					If cdr Is Nothing Then
						Dim cdrText As String = getInstanceDataResult.Value.Replace("&lt;", "<").Replace("&gt;", ">").Replace("<?xml version=""1.0"" encoding=""utf-8""?>", String.Empty)
						cdr = XElement.Load(New StringReader(cdrText))
					End If

					If cdr?.Name = XName.[Get]("CdrServiceGetInstanceData", "http://Cdr.Business.Workflow.Schemas.CdrServiceGetInstanceData") AndAlso CSharpImpl.__Assign(outputs, TryCast(cdr.Element(XName.[Get]("Outputs")), XElement)) IsNot Nothing Then
						Dim returnCodeElement As XElement = outputs.Element("ReturnCode")
						Dim errorCodeElement As XElement = outputs.Element("ErrorCode")
						Dim codeText As String = (If(returnCodeElement, errorCodeElement))?.Value
						Dim returnMessageElement As XElement = outputs.Element("ReturnMessage")
						Dim errorMessageElement As XElement = outputs.Element("ErrorMessage")
						Dim message As String = (If(returnMessageElement, errorMessageElement))?.Value
						Console.WriteLine("Code: " & codeText)
						Console.WriteLine("Message: " & message)
						Dim retcode As Integer? = If(Integer.TryParse(codeText, code), code, Nothing)
						Dim instanceDocuments As XElement = outputs.Element(XName.[Get]("InstanceDocuments"))
						Return New GetInstanceResponse With {
										.Code = code,
										.Message = message,
										.InstanceDocuments = New XDocument(instanceDocuments)
						}
					End If
				End If
			End If

			Return Nothing
		End Function

		Private Class CSharpImpl
			<Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
			Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
				target = value
				Return value
			End Function
		End Class
	End Class
End Namespace
