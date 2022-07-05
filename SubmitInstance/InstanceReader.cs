namespace SubmitInstance
{
	internal class InstanceReader : IInstanceReader
	{
		public string Read(string xmlFileName)
		{
			if (File.Exists(xmlFileName))
				using (StreamReader r = new(xmlFileName))
					return r.ReadToEnd();
			else
			{
				Console.WriteLine($"File {xmlFileName} cannot be found.");
				return string.Empty;
			}
		}
	}
}