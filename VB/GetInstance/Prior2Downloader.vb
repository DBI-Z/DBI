Imports SimpleSOAPClient
Imports SimpleSOAPClient.Helpers
Imports SimpleSOAPClient.Models
Imports System.Threading
Imports System.Xml.Linq

Namespace GetInstance
	Friend Class Prior2Downloader
		Implements IInstanceDownloader

		Private displayer As IDisplayer

		Public Sub New(ByVal displayer As IDisplayer)
			Me.displayer = displayer
		End Sub

		Public Async Function Download(ByVal param As GetInstanceRequest) As Task(Of XDocument) Implements IInstanceDownloader.Download
			Dim prior2 As GetInstanceRequest = New GetInstanceRequest()
			prior2.NumberOfPriorPeriods = param.NumberOfPriorPeriods + 2
			prior2.DataSeriesName = param.DataSeriesName
			prior2.IdRssd = param.IdRssd
			prior2.Password = param.Password
			prior2.ReportingPeriodEndDate = param.ReportingPeriodEndDate
			prior2.Username = param.Username

			Dim requestEnvelope As SoapEnvelope = SoapEnvelope.Prepare().Body(Of GetInstanceRequest)(prior2)
			Dim responseEnvelope As SoapEnvelope = Nothing

			Using client = New SoapHelper().GetInstance()
				responseEnvelope = Await GetResponse(client, requestEnvelope)
			End Using

			If responseEnvelope Is Nothing Then
				Return New XDocument()
			Else
				Return New XDocument(responseEnvelope.Body.Value)
			End If
		End Function

		Private Async Function GetResponse(ByVal client As SoapClient, ByVal requestEnv As SoapEnvelope) As Task(Of SoapEnvelope)
			Try
				Dim responseEnv = Await client.SendAsync("https://cdr.ffiec.gov/cdr/Services/CdrService.asmx", "http://ffiec.gov/cdr/services/GetInstanceData", requestEnv, New CancellationToken())
				Return responseEnv
			Catch ex As Exception
				displayer.WriteLine(ex.Message)
				displayer.WriteLine("Unable to connect to the Internet. Some firewalls require altering permissions to allow EasyCall Report to communicate with the Central Data Repository (CDR).  Your information technology department should be made aware that this communication uses HTTPS via port 443.")
				displayer.WriteLine("Also, the FFIEC CDR system now only supports TLS 1.2;  please insure your operating system supports said.")
			End Try

			Return Nothing
		End Function
	End Class
End Namespace
