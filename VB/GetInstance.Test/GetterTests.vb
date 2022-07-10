Imports System.IO
Imports Moq
Imports Xunit

Namespace GetInstance.Test
	Public Class GetterTests
		<Fact>
		Public Sub Download_AccessDenied_NoWrite()
			Dim downloadStub As Mock(Of IInstanceDownloader) = New Mock(Of IInstanceDownloader)
			Dim writeStub As Mock(Of IInstanceWriter) = New Mock(Of IInstanceWriter)
			Dim extractorStub As Mock(Of IExtractor) = New Mock(Of IExtractor)
			Dim displayerStub As Mock(Of IDisplayer) = New Mock(Of IDisplayer)
			Dim errorInstanceDownloadResult As XDocument = XDocument.Load(New StringReader(ErrorResponse.SoapBody))
			downloadStub.Setup(Function(a) a.Download(It.IsAny(Of GetInstanceRequest)())).ReturnsAsync(errorInstanceDownloadResult)
			writeStub.Setup(Sub(a) a.Write(It.IsAny(Of List(Of WriteFormat))(), It.IsAny(Of Stream)()))
			Dim getter = New InstanceGetter(downloadStub.Object, writeStub.Object, extractorStub.Object, displayerStub.Object)
			Dim getterResult = getter.[Get](TestCredentials)
			writeStub.VerifyNoOtherCalls()
		End Sub

		<Fact>
		Public Sub Download_Basic_Successful()
			Dim downloadStub As Mock(Of IInstanceDownloader) = New Mock(Of IInstanceDownloader)()
			Dim writeStub As Mock(Of IInstanceWriter) = New Mock(Of IInstanceWriter)()
			Dim extractorStub As Mock(Of IExtractor) = New Mock(Of IExtractor)
			Dim displayerStub As Mock(Of IDisplayer) = New Mock(Of IDisplayer)
			Dim successfulInstanceDownloadResult As XDocument = XDocument.Load(New StringReader(SuccessfulEmptyResponse.SoapBody))
			downloadStub.Setup(Function(a) a.Download(It.IsAny(Of GetInstanceRequest)())).ReturnsAsync(successfulInstanceDownloadResult)
			writeStub.Setup(Sub(a) a.Write(It.IsAny(Of List(Of WriteFormat))(), It.IsAny(Of Stream)()))
			Dim getter = New InstanceGetter(downloadStub.Object, writeStub.Object, extractorStub.Object, displayerStub.Object)
			Dim getterResult = getter.[Get](TestCredentials)
		End Sub

		<Fact>
		Public Sub Download_Write_Empty()
			Dim downloadStub As Mock(Of IInstanceDownloader) = New Mock(Of IInstanceDownloader)()
			Dim writeStub As Mock(Of IInstanceWriter) = New Mock(Of IInstanceWriter)()
			Dim extractorStub As Mock(Of IExtractor) = New Mock(Of IExtractor)
			Dim displayerStub As Mock(Of IDisplayer) = New Mock(Of IDisplayer)
			Dim successfulInstanceDownloadResult As XDocument = XDocument.Load(New StringReader(SuccessfulEmptyResponse.SoapBody))
			downloadStub.Setup(Function(a) a.Download(It.IsAny(Of GetInstanceRequest)())).ReturnsAsync(successfulInstanceDownloadResult)
			writeStub.Setup(Sub(a) a.Write(It.IsAny(Of List(Of WriteFormat))(), It.IsAny(Of Stream)()))
			extractorStub.Setup(Function(a) a.Extract(It.IsAny(Of XDocument)())).Returns(New List(Of WriteFormat)())
			Dim getter = New InstanceGetter(downloadStub.Object, writeStub.Object, extractorStub.Object, displayerStub.Object)
			Dim getterResult = getter.[Get](TestCredentials)
			writeStub.Verify(Sub(a) a.Write(It.[Is](Of List(Of WriteFormat))(Function(b) b.Count = 0), It.IsAny(Of Stream)()))
		End Sub

		<Fact>
		Public Sub Download_Write_Multiple()
			Dim downloadStub As Mock(Of IInstanceDownloader) = New Mock(Of IInstanceDownloader)
			Dim writeStub As Mock(Of IInstanceWriter) = New Mock(Of IInstanceWriter)
			Dim displayerStub As Mock(Of IDisplayer) = New Mock(Of IDisplayer)
			Dim soapBody As XDocument = XDocument.Load(New StringReader(SuccessfulResponse1.SoapBody))
			downloadStub.Setup(Function(a) a.Download(It.IsAny(Of GetInstanceRequest)())).ReturnsAsync(soapBody)
			'writeStub.Setup(a => a.Write(It.IsAny<List<WriteFormat>>(), It.IsAny<Stream>()));
			writeStub.Setup(Sub(a) a.Write(It.IsAny(Of List(Of WriteFormat))(), It.IsAny(Of Stream)()))
			Dim getter = New InstanceGetter(downloadStub.Object, writeStub.Object, New Extractor(displayerStub.Object), displayerStub.Object)
			getter.[Get](TestCredentials)
			writeStub.Verify(Sub(a) a.Write(It.[Is](Of List(Of WriteFormat))(Function(b) b.Count = 5417), It.IsAny(Of Stream)()))
			writeStub.VerifyNoOtherCalls()
		End Sub

		<Fact>
		Public Sub Download_Write_SkipCurrentPeriod()
			Dim downloadStub As Mock(Of IInstanceDownloader) = New Mock(Of IInstanceDownloader)
			Dim writeStub As Mock(Of IInstanceWriter) = New Mock(Of IInstanceWriter)
			Dim displayerStub As Mock(Of IDisplayer) = New Mock(Of IDisplayer)
			Dim priorPeriodsRecordCount As Integer = SuccessfulResponse2.RecordCount - SuccessfulResponse2.RecordCountPeriod20220331
			Dim soapBody As XDocument = XDocument.Load(New StringReader(SuccessfulResponse2.SoapBody))
			downloadStub.Setup(Function(a) a.Download(It.IsAny(Of GetInstanceRequest)())).ReturnsAsync(soapBody)
			writeStub.Setup(Sub(a) a.Write(It.IsAny(Of List(Of WriteFormat))(), It.IsAny(Of Stream)()))
			Dim getter = New InstanceGetter(downloadStub.Object, writeStub.Object, New Extractor(displayerStub.Object), displayerStub.Object)
			getter.[Get](TestCredentials)
			writeStub.Verify(Sub(a) a.Write(It.[Is](Of List(Of WriteFormat))(Function(b) b.Count = priorPeriodsRecordCount), It.IsAny(Of Stream)()))
			writeStub.VerifyNoOtherCalls()
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
	End Class
End Namespace
