using System.Diagnostics;
using GetInstance;
using System.Globalization;

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
		Console.WriteLine("There should be exactly " + expectedArgCount + " arguments. Example:");
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
			Console.WriteLine("NumberOfPriorPeriods should be a number. Example:");
			PrintArgs(TestInput.TestRequest);
			return -1;

		}
		if (DateTime.TryParseExact(args[5], GetInstanceRequest.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime endDate))
		{
			cmdLineInput.ReportingPeriodEndDate = endDate;
		}
		else
		{
			Console.WriteLine($"ReportingPeriodEndDate should have date format {GetInstanceRequest.DateFormat}. Example:");
			PrintArgs(TestInput.TestRequest);
			return -1;
		}
		param = cmdLineInput;
	}
}

void PrintArgs(GetInstanceRequest args)
{
	string exeName = Process.GetCurrentProcess().MainModule.ModuleName;
	Console.Write(exeName + " ");
	Console.Write(args.Username + " ");
	Console.Write(args.Password + " ");
	Console.Write(args.DataSeriesName + " ");
	Console.Write(args.IdRssd + " ");
	Console.Write(args.NumberOfPriorPeriods + " ");
	Console.Write(args.ReportingPeriodEndDate.ToString(GetInstanceRequest.DateFormat) + " ");
	Console.WriteLine(string.Empty);
	Console.WriteLine($"{exeName} Username Password DataSeriesName IdRSSD NumberOfPriorPeriods ReportingPeriodEndDate");
}

Console.WriteLine(string.Empty);
Console.WriteLine("Updating history with prior quarter data from the CDR...");
Console.WriteLine("1. Logging on to CDR: ");
await new InstanceGetter(new Prior2Downloader(), new CsvWriter(), new Extractor()).Get(param);

return 0;

public class WriteFormat
{
	public string MtMdrm { get; set; }
	public string MtUnitRef { get; set; }
	public string MtDecimals { get; set; }
	public string MtContextRef { get; set; }
	public string MtData { get; set; }
}

