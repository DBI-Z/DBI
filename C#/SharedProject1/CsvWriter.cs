namespace GetInstance
{
	public class CsvWriter : IInstanceWriter
	{
		IDisplayer displayer;
		public CsvWriter(IDisplayer displayer)
		{
			this.displayer = displayer;
		}

		public void Write(IEnumerable<WriteFormat> records, Stream instanceFile)
		{
			using (StreamWriter sw = new(instanceFile))
				foreach (WriteFormat a in records)
				{
					string csv = string.Join(',', Quoted(a.MtMdrm), Quoted(a.MtContextRef), Quoted(a.MtUnitRef), Quoted(a.MtDecimals), Quoted(a.MtData));
					displayer.WriteLine(csv);
					System.Diagnostics.Debug.WriteLine(csv);
					sw.WriteLine(csv);
				}
		}

		string Quoted(string text) => '"' + text + '"';
	}
}
