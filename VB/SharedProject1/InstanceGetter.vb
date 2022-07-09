Imports System.Globalization
Imports System.IO
Imports System.Text

Namespace GetInstance
	Public Class InstanceGetter
		Private writer As IInstanceWriter
		Private downloader As IInstanceDownloader
		Private extractor As IExtractor
		Private displayer As IDisplayer

		Public Sub New(ByVal downloader As IInstanceDownloader, ByVal writer As IInstanceWriter, ByVal extractor As IExtractor, ByVal displayer As IDisplayer)
			Me.writer = writer
			Me.downloader = downloader
			Me.extractor = extractor
			Me.displayer = displayer
		End Sub

		Public Async Function [Get](ByVal param As GetInstanceRequest) As Task
			Dim appFolder As String = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)
			Dim responseBody As XDocument = Await downloader.Download(param)
			displayer.WriteLine(responseBody.ToString())
			Dim response As GetInstanceResponse = New Cdr(displayer).ExtractGetResponse(responseBody)
			Dim IsSuccessfulGetInstance As Boolean = response?.Code = 0
			Dim xbrlEndTime As DateTime = Nothing

			If IsSuccessfulGetInstance Then
				displayer.WriteLine("3. Processing CDR Data: ")
				Dim ins As IEnumerable(Of XElement) = response.InstanceDocuments.Element("InstanceDocuments").Elements("InstanceDocument")
				Dim wf As List(Of WriteFormat) = New List(Of WriteFormat)

				For Each instanceDocument As XElement In ins
					Dim xbrl As XDocument = New XDocument(instanceDocument.Element(XName.[Get]("xbrl", "http://www.xbrl.org/2003/instance")))
					Dim period As String = GetPeriod(xbrl)

					If DateTime.TryParseExact(period, GetInstanceRequest.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, xbrlEndTime) Then

						If xbrlEndTime.Date = param.ReportingPeriodEndDate.Date Then
							displayer.WriteLine($"Skipping processing of current period: {period}")
							Continue For
						End If
					Else
						displayer.WriteLine($"Cannot read period as date: {period}")
					End If

					Dim thisWf As List(Of WriteFormat) = extractor.Extract(xbrl)
					wf.AddRange(thisWf)
				Next

				Dim currentPeriod As String = param.ReportingPeriodEndDate.ToString(GetInstanceRequest.DateFormat)
				Dim instanceFileName As String = Path.Combine(appFolder, currentPeriod & "-Instance.txt")

				Dim instanceFile As FileStream = New FileStream(instanceFileName, FileMode.Create)
				writer.Write(wf, instanceFile)

				displayer.WriteLine("Completed")
				displayer.WriteLine("CDR Live Update: Successful")
				displayer.WriteLine("Prior quarter history data has been downloaded successfully.")
			Else
				Dim interpretedErrorMessage As String = PrepareMsg(responseBody.ToString())
				AskPreviousFile("2022-Instance.txt")
			End If
		End Function

		Private Function PrepareMsg(ByVal responseErrorMessage As String) As String
			Const defaultErrorCode As Integer = 6
			Dim errorPhrases(4) As (Phrase As String, Code As Integer)
			errorPhrases(0) = ("not authorized", 1)
			errorPhrases(1) = ("not match", 2)
			errorPhrases(2) = ("Access denied", 3)
			errorPhrases(3) = ("ID_RSSD", 4)

			Dim sb As StringBuilder = New StringBuilder()
			Dim errorCode As Integer = defaultErrorCode

			For i As Integer = 0 To errorPhrases.Length - 1
				If responseErrorMessage.IndexOf(errorPhrases(i).Phrase) > 0 Then errorCode = i
			Next

			sb.AppendLine("A problem was encountered while attempting to contact the CDR to download prior quarter history data.")

			Select Case errorCode
				Case 1, 4, 6
					sb.AppendJoin("Error", errorCode, responseErrorMessage)
				Case 2, 3
					sb.AppendLine("Invalid User Name or Password, or User is locked.")
			End Select

			sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.")
			Return sb.ToString()
		End Function

		Private Sub AskPreviousFile(ByVal instanceFileName As String)
			If File.Exists(instanceFileName) Then
				displayer.WriteLine("Although the Live CDR Update was not successful, prior quarter data is available on your hard drive.")
				displayer.WriteLine("It should be safe to use this data in verifying your Call Report.")
				displayer.WriteLine("Do you want to continue Updating History with this prior quarter data? Y/N")
				Dim yn As String = String.Empty
				Dim isaYes As Func(Of Boolean) = Function() String.Compare("Y", yn?.Trim(), ignoreCase:=True) = 0
				Dim isaNo As Func(Of Boolean) = Function() String.Compare("N", yn?.Trim(), ignoreCase:=True) = 0

				While Not isaYes()
					yn = Console.ReadLine()

					If isaNo() Then
						displayer.WriteLine("Update History was not completed.")
						displayer.WriteLine("Live CDR Update Error")
						Exit While
					End If
				End While
			Else
				displayer.WriteLine("Update History was not completed.")
			End If
		End Sub

		Private Function GetPeriod(ByVal xbrlRoot As XDocument) As String
			Const defaultNamespace As String = "http://www.xbrl.org/2003/instance"
			Dim xbrl As XElement = Nothing, endDate As String = Nothing

			If CSharpImpl.__Assign(xbrl, TryCast(xbrlRoot.Element(XName.[Get]("xbrl", defaultNamespace)), XElement)) IsNot Nothing Then

				For Each context As XElement In xbrl.Elements(XName.[Get]("context", defaultNamespace))
					If CSharpImpl.__Assign(endDate, TryCast(context.Element(XName.[Get]("period", defaultNamespace))?.Element(XName.[Get]("endDate", defaultNamespace))?.Value, String)) IsNot Nothing Then Return endDate
				Next
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
