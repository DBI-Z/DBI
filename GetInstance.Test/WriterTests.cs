using Moq;
using GetInstance;

namespace GetInstance.Test
{
	public class WriterTests
	{
		[Fact]
		public void Write_Sample_Quoted()
		{
			//Arrange
			var writer = new CsvFileWriter();
			MemoryStream csv = new();
			WriteFormat[] writeFormats =
			{
				new WriteFormat { MtMdrm = "1", MtContextRef = "2", MtUnitRef = "3", MtDecimals = "4", MtData = "5" },
				new WriteFormat { MtMdrm = "", MtContextRef = "", MtUnitRef = "", MtDecimals = "", MtData = "" },
				new WriteFormat { MtMdrm = "a", MtContextRef = "b", MtUnitRef = "c", MtDecimals = "d", MtData = "e" },
			};
		//Act
		writer.Write(writeFormats, csv);
			
			//csv.Seek(0, SeekOrigin.Begin);
			string[] lines = new string[writeFormats.Length];

		bool a = csv.TryGetBuffer(out ArraySegment<byte> buffer);
			using (StreamReader sr = new (new MemoryStream(buffer.Array)))
				for (int i = 0; i<lines.Length; i++)
					lines[i] = sr.ReadLine();

			//Assert
			string[] expected =
			{
				@"""1"",""2"",""3"",""4"",""5""",
				@""""","""","""","""",""""",
				@"""a"",""b"",""c"",""d"",""e"""
			};

			for (int i = 0; i<expected.Length; i++)
				Assert.Equal(expected[i], lines[i]);
		}
	} 
}