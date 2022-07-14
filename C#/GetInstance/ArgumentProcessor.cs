using System.Diagnostics;
using System.Globalization;

namespace GetInstance
{
	internal class ArgumentProcessor
	{
		string[] args;
		IDisplayer displayer;

		public ArgumentProcessor(string[] args, IDisplayer displayer)
		{
			this.args = args;
			this.displayer = displayer;
		}

		public GetInstanceRequest GetRequest()
		{
			const int expectedArgCount = 6;
			GetInstanceRequest request;

			if (args.Length == expectedArgCount)
			{
				request = new GetInstanceRequest
				{
					Username = args[0],
					Password = args[1],
					DataSeriesName = args[2],
					IdRssd = args[3]
				};
				if (int.TryParse(args[4], out int periods))
				{
					request.NumberOfPriorPeriods = periods;
					if (DateTime.TryParseExact(args[5], GetInstanceRequest.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime endDate))
					{
						request.ReportingPeriodEndDate = endDate;
					}
					else
					{
						displayer.WriteLine($"ReportingPeriodEndDate should have date format {GetInstanceRequest.DateFormat}. Example:");
						PrintArgs(TestRequest);
						request = null;
					}
				}
				else
				{
					displayer.WriteLine("NumberOfPriorPeriods should be a number. Example:");
					PrintArgs(TestRequest);
					request = null;
				}
			}
			else if (args.Length == 0)
			{
				request = null;
			}
			else { 
				displayer.WriteLine("There should be exactly " + expectedArgCount + " arguments. Example:");
				PrintArgs(TestRequest);
				request = null;
			}
			return request;
		}

		public void PrintArgs(GetInstanceRequest args)
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

		public static GetInstanceRequest TestRequest =>
			new GetInstanceRequest
			{
				Username = "DBIFINAN2",
				Password = "Testingnewcode7~",
				DataSeriesName = "call",
				ReportingPeriodEndDate = new DateTime(2022, 03, 31),
				IdRssd = "141958",
				NumberOfPriorPeriods = 6
			};
	}
}
