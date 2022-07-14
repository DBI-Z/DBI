namespace SubmitInstance
{
	public interface ISettings
	{
		void Load(Stream stream);
		string GetAction { get; }
		string Url { get; }
		string UrlT { get; }
		string NS { get; }
		string SubmitAction { get; }
		string TestSubmitAction { get; } 
	}
}
