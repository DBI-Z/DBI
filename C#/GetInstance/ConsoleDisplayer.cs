namespace GetInstance
{
	internal class ConsoleDisplayer : IDisplayer
	{
		public void Write(string text)
		{
			Console.Write(text);
		}

		public void WriteLine(string text)
		{
			Console.WriteLine(text);
		}
	}
}
