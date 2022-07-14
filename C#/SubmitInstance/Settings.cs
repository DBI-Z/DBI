namespace SubmitInstance
{
	public class Settings : ISettings
	{
		public const string SubmitRequestProd = "SubmitInstanceData";
		public const string SubmitRequestTest = "SubmitTestInstanceData";
		public const string SubmitResponseProd = "SubmitInstanceDataResponse";
		public const string SubmitResponseTest = "SubmitTestInstanceDataResponse";
		public const string SubmitResultProd = "SubmitInstanceDataResult";
		public const string SubmitResultTest = "SubmitTestInstanceDataResult";

		public string GetAction => GetValue("GetAction");
		public string Url => GetValue("URL");
		public string UrlT => GetValue("URLT");
		public string NS => GetValue("NS");
		public string SubmitAction => GetValue("SubmitAction");
		public string TestSubmitAction => GetValue("TestSubmitAction");

		string GetValue(string key)
		{
			if (string.IsNullOrWhiteSpace(key))
				throw new ArgumentNullException(nameof(key));
			else if (settings == null)
				throw new InvalidOperationException(nameof(Load) + "must be called before reading a value");
			else
			{
				string loweredKey = key.ToLower();
				if (settings.TryGetValue(loweredKey, out string value))
					return value;
				else
					return string.Empty;
			}
		}

		Dictionary<string, string> settings;

		public void Load(Stream stream)
		{
			StreamReader sr = new(stream);
			settings = new();
			while (sr.ReadLine() is string line)
			{
				string[] keyValue = line.Split('=', StringSplitOptions.TrimEntries);
				string loweredKey = keyValue[0].ToLower();
				settings[loweredKey] = keyValue[1];
				//Console.WriteLine($"Loaded {loweredKey}={keyValue[1]}");
			}
		}
	}
}