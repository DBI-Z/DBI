Imports System.IO
Imports System.Net.Http
Imports System.Text
Imports System.Xml.Serialization

Public Class Submitter
		Private reader As IInstanceReader
		Private poster As IInstancePoster
		Private settings As ISettings
	Private builder As ISubmitRequestBuilder
	Private displayer As IDisplayer

	Public Sub New(reader As IInstanceReader, poster As IInstancePoster, settings As ISettings, builder As ISubmitRequestBuilder, displayer As IDisplayer)
		Me.reader = reader
		Me.poster = poster
		Me.settings = settings
		Me.builder = builder
		Me.displayer = displayer
	End Sub

	Public Async Function Submit(ByVal param As SubmitParam) As Task(Of Boolean)
		displayer.WriteLine("Submitting Call Report to the CDR")
		displayer.WriteLine("1. Logging on to CDR")
		Dim responseBody As XDocument = Nothing

		Try
			Dim fileContents As String = reader.Read(param.Filename)
			If String.IsNullOrWhiteSpace(fileContents) Then
				displayer.WriteLine("File not found or empty: " & param.Filename)
				Return False
			End If

			Dim url As String = If(param.Prod, settings.Url, settings.UrlT)
			Dim action As String = If(param.Prod, settings.SubmitAction, settings.TestSubmitAction)
			displayer.WriteLine("2. Submitting Call Report: ")
			displayer.WriteLine(url)
			displayer.WriteLine(action)

			If String.IsNullOrWhiteSpace(fileContents) Then
				Return False
			End If

			Dim submitBody As XDocument = builder.Build(param, settings.NS, fileContents)
			responseBody = Await poster.Post(url, action, submitBody)
			displayer.WriteLine(responseBody.ToString())
			Dim response = New Cdr().ExtractSubmitResponse(responseBody, param.Prod)
			Dim IsSuccessfulGetInstance As Boolean = response.ReturnCode = 0

			If IsSuccessfulGetInstance Then
				Return True
			Else
				displayer.WriteLine("Code: " & response.ReturnCode)
				displayer.WriteLine("Message: " & response.ReturnMessage)
				Return False
			End If

		Catch ex As IOException
			displayer.WriteLine(ex.Message)
			displayer.WriteLine($"Error while reading file {param.Filename}")
			Return False
		Catch ex As HttpRequestException
			displayer.WriteLine(ex.Message)
			Dim errmsg As String = "Unable to connect. Some firewalls require altering permissions to allow EasyCall Report to communicate with the Central Data Repository.  Your information technology department should be made aware that this communication uses HTTPS via port 443." & "Also, the FFIEC CDR system now only supports TLS 1.2;  please insure your operating system supports said."
			displayer.WriteLine(PrepareMsg(errmsg))
			Return False
		Catch ex As Exception
			displayer.WriteLine(ex.Message)
			Dim interpretedErrorMessage As String = PrepareMsg(If(responseBody?.ToString(), String.Empty))
			displayer.WriteLine(interpretedErrorMessage)
			Return False
			End Try
		End Function

		Private Function PrepareMsg(ByVal subText As String) As String
			Const defaultErrorCode As Integer = 6
			Dim errorPhrases() As (Phrase As String, Code As Integer) =
			{
						("not authorized", 1),
						("not match", 2),
						("Access denied", 3),
						("valid XML", 4),
						("ID_RSSD", 5)
				}
			Dim sb As StringBuilder = New StringBuilder()
			Dim errorCode As Integer = defaultErrorCode

			For i As Integer = 0 To errorPhrases.Length - 1
				If subText.IndexOf(errorPhrases(i).Phrase) > 0 Then errorCode = i
			Next

			sb.AppendLine("A problem was encountered while attempting to submit your Call Report to the CDR.")

			Select Case errorCode
				Case 1
					sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.")
				Case 4
					sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.")
				Case 5, defaultErrorCode
					sb.AppendJoin("Error", errorCode, subText)
				Case 2
					sb.AppendLine("Invalid User Name or Password, or User is locked.")
					sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.")
				Case 3
					sb.AppendLine("Invalid User Name or Password, User is locked, or mandatory training must be completed.")
					sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.")
			End Select

			sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.")
			Return sb.ToString()
		End Function
	End Class

<XmlRoot("SubmitInstanceData", [Namespace]:="http://ffiec.gov/cdr/services/")>
Public Class SubmitInstanceDataRequest
	<XmlElement("userName")>
	Public Property UserName As String
	<XmlElement("password")>
	Public Property Password As String
	<XmlElement("instanceData")>
	Public Property InstanceData As String
End Class