using Moq;
using System.Diagnostics;
using System.Xml.Linq;

namespace GetInstance.Test
{
	public class GetterTests
	{
		//private readonkly Mock<IInstanceDownloader> r = 

		[Fact]
		public void Download_Basic_Successful()
		{
			//Arrange
			Mock<IInstanceDownloader> downloadStub = new Mock<IInstanceDownloader>();
			Mock<IInstanceWriter> writeStub = new Mock<IInstanceWriter>();

			XDocument successfulInstanceDownloadResult = XDocument.Load(new StringReader(SuccessfulEmptyResponse.SoapBody));

			downloadStub.Setup(a => a.Download(It.IsAny<GetInstanceRequest>())).ReturnsAsync(successfulInstanceDownloadResult);

			writeStub.Setup(a => a.Write(It.IsAny<List<WriteFormat>>(), It.IsAny<string>()));

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
			writeStub.Setup(a => a.Write(It.IsAny<List<WriteFormat>>(), It.IsAny<string>()));
			var getter = new InstanceGetter(downloadStub.Object, writeStub.Object);

			//Act
			var getterResult = getter.Do(TestCredentials);

			//Assert? 
			writeStub.Verify(a => a.Write(It.Is<List<WriteFormat>>(b => b.Count == 0), It.IsAny<string>())); 
		}

		[Fact]
		public void Download_Write_Multiple()
		{
			//Arrange
			Mock<IInstanceDownloader> downloadStub = new();
			Mock<IInstanceWriter> writeStub = new();
			XDocument soapBody = XDocument.Load(new StringReader(SuccessfulResponse1.SoapBody));
			downloadStub.Setup(a => a.Download(It.IsAny<GetInstanceRequest>())).ReturnsAsync(soapBody);
			writeStub.Setup(a => a.Write(It.IsAny<List<WriteFormat>>(), It.IsAny<string>()));
			var getter = new InstanceGetter(downloadStub.Object, writeStub.Object);

			//Act
			_ = getter.Do(TestCredentials);

			//Assert
			writeStub.Verify(a => a.Write(It.Is<List<WriteFormat>>(b => b.Count == 1259), It.IsAny<string>()));
			writeStub.Verify(a => a.Write(It.Is<List<WriteFormat>>(b => b.Count == 609), It.IsAny<string>()));
			writeStub.Verify(a => a.Write(It.Is<List<WriteFormat>>(b => b.Count == 1090), It.IsAny<string>()));
			writeStub.Verify(a => a.Write(It.Is<List<WriteFormat>>(b => b.Count == 611), It.IsAny<string>()));
			writeStub.Verify(a => a.Write(It.Is<List<WriteFormat>>(b => b.Count == 1237), It.IsAny<string>()));
			writeStub.Verify(a => a.Write(It.Is<List<WriteFormat>>(b => b.Count == 611), It.IsAny<string>())); 
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