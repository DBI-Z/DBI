Imports System.IO
Imports Moq
Imports Xunit

Namespace GetInstance.Test
	Public Class WriterTests
		<Fact>
		Public Sub Write_Sample_Quoted()
			Dim displayerStub As Mock(Of IDisplayer) = New Mock(Of IDisplayer)
			Dim writer = New CsvWriter(displayerStub.Object)
			Dim writeFormats As WriteFormat() = {New WriteFormat With {
																.MtMdrm = "1",
																.MtContextRef = "2",
																.MtUnitRef = "3",
																.MtDecimals = "4",
																.MtData = "5"
												}, New WriteFormat With {
																.MtMdrm = "",
																.MtContextRef = "",
																.MtUnitRef = "",
																.MtDecimals = "",
																.MtData = ""
												}, New WriteFormat With {
																.MtMdrm = "a",
																.MtContextRef = "b",
																.MtUnitRef = "c",
																.MtDecimals = "d",
																.MtData = "e"
												}, New WriteFormat With {
																.MtMdrm = "RCONF233",
																.MtContextRef = "CI_141958_2021",
																.MtUnitRef = "USD",
																.MtDecimals = "0",
																.MtData = "0"
												}, New WriteFormat With {
																.MtMdrm = "RCOA7206",
																.MtContextRef = "CI_141958_2021-12-31",
																.MtUnitRef = "PURE",
																.MtDecimals = "6",
																.MtData = "0"
												}, New WriteFormat With {
																.MtMdrm = "RCONFT09",
																.MtContextRef = "CI_141958_2020-06-30",
																.MtUnitRef = "",
																.MtDecimals = "0",
																.MtData = "false"
												}}
			Dim csv As MemoryStream = New MemoryStream
			writer.Write(writeFormats, csv)
			Dim lines As String() = New String(writeFormats.Length - 1) {}
			Dim buffer As ArraySegment(Of Byte) = Nothing
			csv.TryGetBuffer(buffer)

			Dim sr As StreamReader = New StreamReader(New MemoryStream(buffer.Array))

			For i As Integer = 0 To lines.Length - 1
				lines(i) = sr.ReadLine()
				Debug.WriteLine(lines(i))
			Next

			Dim expected As String() = {"""1"",""2"",""3"",""4"",""5""", """"","""","""","""",""""", """a"",""b"",""c"",""d"",""e"""}

			For i As Integer = 0 To expected.Length - 1
				Assert.Equal(expected(i), lines(i))
			Next
		End Sub

		<Fact>
		Public Sub Convert_XmlToCsv_CorrectFormat()
			Dim displayerStub As Mock(Of IDisplayer) = New Mock(Of IDisplayer)
			Dim writer = New CsvWriter(displayerStub.Object)
			Dim downloadStub As Mock(Of IInstanceDownloader) = New Mock(Of IInstanceDownloader)
			Dim soapBody As XDocument = XDocument.Load(New StringReader(LongXbrl.Sample))
			Dim getter = New InstanceGetter(downloadStub.Object, writer, New Extractor(displayerStub.Object), displayerStub.Object)
			Dim memory As MemoryStream = New MemoryStream()
			Dim extractor = New Extractor(displayerStub.Object)
			Dim xbrl As XDocument = New XDocument(soapBody.Element(XName.[Get]("xbrl", "http://www.xbrl.org/2003/instance")))
			Dim wf As List(Of WriteFormat) = extractor.Extract(xbrl)
			writer.Write(wf, memory)
			Dim buffer As ArraySegment(Of Byte) = Nothing
			memory.TryGetBuffer(buffer)
			Dim line As String
			Dim sr As StreamReader = New StreamReader(New MemoryStream(buffer.Array))
			While (CSharpImpl.__Assign(line, sr.ReadLine())) IsNot Nothing
				Debug.WriteLine(line)
			End While
		End Sub

		Private ReadOnly Property TestCredentials As GetInstanceRequest
			Get
				Return New GetInstanceRequest With {
								.Username = "DBIFINAN2",
								.Password = "TestingNew12",
								.DataSeriesName = "call",
								.ReportingPeriodEndDate = New DateTime(2022, 3, 31),
								.IdRssd = "141958",
								.NumberOfPriorPeriods = 6
				}
			End Get
		End Property

		Private Class CSharpImpl
			<Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
			Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
				target = value
				Return value
			End Function
		End Class
	End Class
End Namespace
