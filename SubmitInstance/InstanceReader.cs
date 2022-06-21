namespace SubmitInstance
{
	internal class InstanceReader : IInstanceReader
	{
		public string Read(string xmlFileName)
		{
			using (StreamReader r = new(xmlFileName))
				return r.ReadToEnd();
		}
	}
}