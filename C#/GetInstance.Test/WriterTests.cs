using Moq;
using System.Xml.Linq;

namespace GetInstance.Test
{
	public class WriterTests
	{
		[Fact]
		public void Write_Sample_Quoted()
		{
			//Arrange
			Mock<IDisplayer> displayerStub = new();
			var writer = new CsvWriter(displayerStub.Object);
			WriteFormat[] writeFormats =
			{
				new WriteFormat { MtMdrm = "1", MtContextRef = "2", MtUnitRef = "3", MtDecimals = "4", MtData = "5" },
				new WriteFormat { MtMdrm = "", MtContextRef = "", MtUnitRef = "", MtDecimals = "", MtData = "" },
				new WriteFormat { MtMdrm = "a", MtContextRef = "b", MtUnitRef = "c", MtDecimals = "d", MtData = "e" },
				new WriteFormat { MtMdrm = "RCONF233", MtContextRef = "CI_141958_2021", MtUnitRef = "USD", MtDecimals = "0", MtData = "0" },
				new WriteFormat { MtMdrm = "RCOA7206", MtContextRef = "CI_141958_2021-12-31", MtUnitRef = "PURE", MtDecimals = "6", MtData = "0" },
				new WriteFormat { MtMdrm = "RCONFT09", MtContextRef = "CI_141958_2020-06-30", MtUnitRef = "", MtDecimals = "0", MtData = "false" },
			};
			//Act
			MemoryStream csv = new();
			writer.Write(writeFormats, csv);
			string[] lines = new string[writeFormats.Length];
			csv.TryGetBuffer(out ArraySegment<byte> buffer);
			using (StreamReader sr = new(new MemoryStream(buffer.Array)))
				for (int i = 0; i < lines.Length; i++)
				{
					lines[i] = sr.ReadLine();
					System.Diagnostics.Debug.WriteLine(lines[i]);
				}
			//Assert
			string[] expected =
			{
				@"""1"",""2"",""3"",""4"",""5""",
				@""""","""","""","""",""""",
				@"""a"",""b"",""c"",""d"",""e"""
			};

			for (int i = 0; i < expected.Length; i++)
				Assert.Equal(expected[i], lines[i]);
		}

		[Fact]
		public void Convert_XmlToCsv_CorrectFormat()
		{
			//Arrange
			Mock<IDisplayer> displayerStub = new();
			var writer = new CsvWriter(displayerStub.Object);
			Mock<IInstanceDownloader> downloadStub = new();
			XDocument soapBody = XDocument.Load(new StringReader(LongXbrl.Sample));
			var getter = new InstanceGetter(downloadStub.Object,  writer, new Extractor(displayerStub.Object), displayerStub.Object);
			MemoryStream memory = new();
			var extractor = new Extractor(displayerStub.Object);

			//Act
			XDocument xbrl = new XDocument(soapBody.Element(XName.Get("xbrl", "http://www.xbrl.org/2003/instance")));
			List<WriteFormat> wf = extractor.Extract(xbrl);
			writer.Write(wf, memory);

			memory.TryGetBuffer(out ArraySegment<byte> buffer);
			string line;
			using (StreamReader sr = new(new MemoryStream(buffer.Array)))
			while((line = sr.ReadLine()) != null)
					System.Diagnostics.Debug.WriteLine(line);

			//Assert  
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