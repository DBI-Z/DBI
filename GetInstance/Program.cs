using System.Diagnostics;
using GetInstance;
using System.Globalization;


ApplicationConfiguration.Initialize();
Form1 f = new();
IDisplayer displayer = new TextBoxDisplayer(f.LogDisplayTextbox);
f.Displayer = displayer;
Application.Run(f);

GetInstanceRequest param;
if (args.Length == 0)
{
	param = TestInput.TestRequest;
	PrintArgs(param);
}
else
{
	const int expectedArgCount = 6;
	if (args.Length != expectedArgCount)
	{
		param = null;
		displayer.WriteLine("There should be exactly " + expectedArgCount + " arguments. Example:");
		PrintArgs(TestInput.TestRequest);
		return -1;
	}
	else
	{
		var cmdLineInput = new GetInstanceRequest
		{
			Username = args[0],
			Password = args[1],
			DataSeriesName = args[2],
			IdRssd = args[3]
		};
		if (int.TryParse(args[4], out int periods))
		{
			cmdLineInput.NumberOfPriorPeriods = periods;
		}
		else
		{
			displayer.WriteLine("NumberOfPriorPeriods should be a number. Example:");
			PrintArgs(TestInput.TestRequest);
			return -1;

		}
		if (DateTime.TryParseExact(args[5], GetInstanceRequest.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime endDate))
		{
			cmdLineInput.ReportingPeriodEndDate = endDate;
		}
		else
		{
			displayer.WriteLine($"ReportingPeriodEndDate should have date format {GetInstanceRequest.DateFormat}. Example:");
			PrintArgs(TestInput.TestRequest);
			return -1;
		}
		param = cmdLineInput;
	}
}

void PrintArgs(GetInstanceRequest args)
{
	string exeName = Process.GetCurrentProcess().MainModule.ModuleName;
	displayer.Write(exeName + " ");
	displayer.Write(args.Username + " ");
	displayer.Write(new string('*', args.Password.Length) + " ");
	displayer.Write(args.DataSeriesName + " ");
	displayer.Write(args.IdRssd + " ");
	displayer.Write(args.NumberOfPriorPeriods + " ");
	displayer.Write(args.ReportingPeriodEndDate.ToString(GetInstanceRequest.DateFormat) + " ");
	displayer.WriteLine(string.Empty);
	displayer.WriteLine($"{exeName} Username Password DataSeriesName IdRSSD NumberOfPriorPeriods ReportingPeriodEndDate");
}

displayer.WriteLine(string.Empty);
displayer.WriteLine("Updating history with prior quarter data from the CDR...");
displayer.WriteLine("1. Logging on to CDR: ");
await new InstanceGetter(new Prior2Downloader(displayer), new CsvWriter(displayer), new Extractor(displayer), displayer).Get(param);

return 0;

public class WriteFormat
{
	public string MtMdrm { get; set; }
	public string MtUnitRef { get; set; }
	public string MtDecimals { get; set; }
	public string MtContextRef { get; set; }
	public string MtData { get; set; }
}

