namespace GetInstance
{
	public class CsvFileWriter : IInstanceWriter
	{
		public void Write(IEnumerable<WriteFormat> records, Stream instanceFile)
		{
			using (StreamWriter sw = new(instanceFile))
				foreach (WriteFormat a in records)
				{
					string csv = string.Join(',', Quoted(a.MtMdrm), Quoted(a.MtContextRef), Quoted(a.MtUnitRef), Quoted(a.MtDecimals), Quoted(a.MtData));
					Console.WriteLine(csv);
					sw.WriteLine(csv);
				}
		}

		string Quoted(string text) => '"' + text + '"';
	}
}
