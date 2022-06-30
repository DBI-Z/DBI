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
			XDocument errorInstanceDownloadResult = XDocument.Load(new StringReader(ErrorResponse.SoapBody));
			downloadStub.Setup(a => a.Download(It.IsAny<GetInstanceRequest>())).ReturnsAsync(errorInstanceDownloadResult);
			writeStub.Setup(a => a.Write(It.IsAny<List<WriteFormat>>(), It.IsAny<Stream>()));
			var getter = new InstanceGetter(downloadStub.Object, writeStub.Object);

			//Act
			var getterResult = getter.Do(TestCredentials);
			writeStub.VerifyNoOtherCalls();
		}

		[Fact]
		public void Download_Basic_Successful()
		{
			//Arrange
			Mock<IInstanceDownloader> downloadStub = new Mock<IInstanceDownloader>();
			Mock<IInstanceWriter> writeStub = new Mock<IInstanceWriter>();

			XDocument successfulInstanceDownloadResult = XDocument.Load(new StringReader(SuccessfulEmptyResponse.SoapBody));

			downloadStub.Setup(a => a.Download(It.IsAny<GetInstanceRequest>())).ReturnsAsync(successfulInstanceDownloadResult);

			writeStub.Setup(a => a.Write(It.IsAny<List<WriteFormat>>(), It.IsAny<Stream>()));

			var getter = new InstanceGetter(downloadStub.Object, writeStub.Object);
			//Act
			var getterResult = getter.Do(TestCredentials);

			//Assert
			//Complete without exception.
		}

		[Fact]
		public void Download_Write_Empty()
		{
			//Arrange
			Mock<IInstanceDownloader> downloadStub = new Mock<IInstanceDownloader>();
			Mock<IInstanceWriter> writeStub = new Mock<IInstanceWriter>();
			XDocument successfulInstanceDownloadResult = XDocument.Load(new StringReader(SuccessfulEmptyResponse.SoapBody));
			downloadStub.Setup(a => a.Download(It.IsAny<GetInstanceRequest>())).ReturnsAsync(successfulInstanceDownloadResult);
			writeStub.Setup(a => a.Write(It.IsAny<List<WriteFormat>>(), It.IsAny<Stream>()));
			var getter = new InstanceGetter(downloadStub.Object, writeStub.Object);

			//Act
			var getterResult = getter.Do(TestCredentials);

			//Assert? 
			writeStub.Verify(a => a.Write(It.Is<List<WriteFormat>>(b => b.Count == 0), It.IsAny<Stream>()));
		}

		[Fact]
		public void Download_Write_Multiple()
		{
			//Arrange
			Mock<IInstanceDownloader> downloadStub = new();
			Mock<IInstanceWriter> writeStub = new();
			XDocument soapBody = XDocument.Load(new StringReader(SuccessfulResponse1.SoapBody));
			downloadStub.Setup(a => a.Download(It.IsAny<GetInstanceRequest>())).ReturnsAsync(soapBody);
			writeStub.Setup(a => a.Write(It.IsAny<List<WriteFormat>>(), It.IsAny<Stream>()));
			var getter = new InstanceGetter(downloadStub.Object, writeStub.Object);

			//Act
			_ = getter.Do(TestCredentials);

			//Assert
			writeStub.Verify(a => a.Write(It.Is<List<WriteFormat>>(b => b.Count == 1259), It.IsAny<Stream>()));
			writeStub.Verify(a => a.Write(It.Is<List<WriteFormat>>(b => b.Count == 609), It.IsAny<Stream>()));
			writeStub.Verify(a => a.Write(It.Is<List<WriteFormat>>(b => b.Count == 1090), It.IsAny<Stream>()));
			writeStub.Verify(a => a.Write(It.Is<List<WriteFormat>>(b => b.Count == 611), It.IsAny<Stream>()));
			writeStub.Verify(a => a.Write(It.Is<List<WriteFormat>>(b => b.Count == 1237), It.IsAny<Stream>()));
			writeStub.Verify(a => a.Write(It.Is<List<WriteFormat>>(b => b.Count == 611), It.IsAny<Stream>())); 
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