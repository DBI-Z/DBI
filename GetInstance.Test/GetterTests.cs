using Moq;
using System.Xml.Linq;

namespace GetInstance.Test
{
	public class GetterTests
	{
		[Fact]
		public void Download_AccessDenied_NoWrite()
		{
			//Arrange
			Mock<IInstanceDownloader> downloadStub = new();
			Mock<IInstanceWriter> writeStub = new();
			Mock<IExtractor> extractorStub = new();
			Mock<IDisplayer> displayerStub = new();
			XDocument errorInstanceDownloadResult = XDocument.Load(new StringReader(ErrorResponse.SoapBody));
			downloadStub.Setup(a => a.Download(It.IsAny<GetInstanceRequest>())).ReturnsAsync(errorInstanceDownloadResult);
			writeStub.Setup(a => a.Write(It.IsAny<List<WriteFormat>>(), It.IsAny<Stream>()));
			var getter = new InstanceGetter(downloadStub.Object, writeStub.Object, extractorStub.Object, displayerStub.Object);

			//Act
			var getterResult = getter.Get(TestCredentials);
			writeStub.VerifyNoOtherCalls();
		}

		[Fact]
		public void Download_Basic_Successful()
		{
			//Arrange
			Mock<IInstanceDownloader> downloadStub = new Mock<IInstanceDownloader>();
			Mock<IInstanceWriter> writeStub = new Mock<IInstanceWriter>();
			Mock<IExtractor> extractorStub = new();
			Mock<IDisplayer> displayerStub = new();

			XDocument successfulInstanceDownloadResult = XDocument.Load(new StringReader(SuccessfulEmptyResponse.SoapBody));

			downloadStub.Setup(a => a.Download(It.IsAny<GetInstanceRequest>())).ReturnsAsync(successfulInstanceDownloadResult);

			writeStub.Setup(a => a.Write(It.IsAny<List<WriteFormat>>(), It.IsAny<Stream>()));

			var getter = new InstanceGetter(downloadStub.Object, writeStub.Object, extractorStub.Object, displayerStub.Object);
			//Act
			var getterResult = getter.Get(TestCredentials);

			//Assert
			//Complete without exception.
		}

		[Fact]
		public void Download_Write_Empty()
		{
			//Arrange
			Mock<IInstanceDownloader> downloadStub = new Mock<IInstanceDownloader>();
			Mock<IInstanceWriter> writeStub = new Mock<IInstanceWriter>();
			Mock<IExtractor> extractorStub = new();
			Mock<IDisplayer> displayerStub = new();
			XDocument successfulInstanceDownloadResult = XDocument.Load(new StringReader(SuccessfulEmptyResponse.SoapBody));
			downloadStub.Setup(a => a.Download(It.IsAny<GetInstanceRequest>())).ReturnsAsync(successfulInstanceDownloadResult);
			writeStub.Setup(a => a.Write(It.IsAny<List<WriteFormat>>(), It.IsAny<Stream>()));
			extractorStub.Setup(a => a.Extract(It.IsAny<XDocument>())).Returns(new List<WriteFormat>());
			var getter = new InstanceGetter(downloadStub.Object, writeStub.Object, extractorStub.Object
			, displayerStub.Object);

			//Act
			var getterResult = getter.Get(TestCredentials);

			//Assert? 
			writeStub.Verify(a => a.Write(It.Is<List<WriteFormat>>(b => b.Count == 0), It.IsAny<Stream>()));
		}

		[Fact]
		public void Download_Write_Multiple()
		{
			//Arrange
			Mock<IInstanceDownloader> downloadStub = new();
			Mock<IInstanceWriter> writeStub = new();
			Mock<IDisplayer> displayerStub = new();
			XDocument soapBody = XDocument.Load(new StringReader(SuccessfulResponse1.SoapBody));
			downloadStub.Setup(a => a.Download(It.IsAny<GetInstanceRequest>())).ReturnsAsync(soapBody);
			//extractorStub.Setup(a => a.Extract(It.IsAny<XDocument>()));
			writeStub.Setup(a => a.Write(It.IsAny<List<WriteFormat>>(), It.IsAny<Stream>()));
			var getter = new InstanceGetter(downloadStub.Object, writeStub.Object, new Extractor(displayerStub.Object), displayerStub.Object);

			//Act
			_ = getter.Get(TestCredentials);

			//Assert
			writeStub.Verify(a => a.Write(It.Is<List<WriteFormat>>(b => b.Count == 5417), It.IsAny<Stream>()));
			writeStub.VerifyNoOtherCalls();
		}

		[Fact]
		public void Download_Write_SkipCurrentPeriod()
		{
			//Arrange
			Mock<IInstanceDownloader> downloadStub = new();
			Mock<IInstanceWriter> writeStub = new();
			Mock<IDisplayer> displayerStub = new();
			int priorPeriodsRecordCount = SuccessfulResponse2.RecordCount - SuccessfulResponse2.RecordCountPeriod20220331;

			XDocument soapBody = XDocument.Load(new StringReader(SuccessfulResponse2.SoapBody));
			downloadStub.Setup(a => a.Download(It.IsAny<GetInstanceRequest>())).ReturnsAsync(soapBody);
			writeStub.Setup(a => a.Write(It.IsAny<List<WriteFormat>>(), It.IsAny<Stream>()));
			var getter = new InstanceGetter(downloadStub.Object, writeStub.Object, new Extractor(displayerStub.Object), displayerStub.Object);

			//Act
			_ = getter.Get(TestCredentials);

			//Assert
			writeStub.Verify(a => a.Write(It.Is<List<WriteFormat>>(b => b.Count == priorPeriodsRecordCount), It.IsAny<Stream>()));
			writeStub.VerifyNoOtherCalls();
		}


		GetInstanceRequest TestCredentials => new GetInstanceRequest
		{
			Username = "DBIFINAN2",
			Password = "TestingNew12",
			DataSeriesName = "call",
			ReportingPeriodEndDate = new DateTime(2022, 03, 31),
			IdRssd = "141958",
			NumberOfPriorPeriods = 6
		};
	}
}