using Moq;
using System.Xml.Linq;

namespace GetInstance.Test
{
	public class GetterTests
	{
		//private readonkly Mock<IInstanceDownloader> r = 

		[Fact]
		public async Task Download_Basic_Successful()
		{
			//Arrange
			Mock<IInstanceDownloader> downloadStub = new Mock<IInstanceDownloader>();
			Mock<IInstanceWriter> writeStub = new Mock<IInstanceWriter>();

			XDocument successfulInstanceDownloadResult = XDocument.Load(new StringReader(SuccessfulResponse.SoapBody));

			downloadStub.Setup(a => a.Download(It.IsAny<GetInstanceRequest>())).ReturnsAsync(successfulInstanceDownloadResult);

			writeStub.Setup(a => a.Write(It.IsAny<List<WriteFormat>>(), It.IsAny<string>()));

			var getter = new InstanceGetter(downloadStub.Object, writeStub.Object);
			//Act
			var getterResult = getter.Do(TestCredentials);

			//Assert
			//Complete without exception.
		}

		[Fact]
		public async Task Download_Write_Anydata()
		{

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