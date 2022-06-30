using SubmitInstance;
using System.Diagnostics;

const string dateFormat = "yyyy-MM-dd";
const int expectedArgCount = 4;

SubmitParam param;
if (args.Length == 0)
{
	param = TestInput.TestParam;
}
else if (args.Length == expectedArgCount)
{
	param = new()
	{
		Username = args[0],
		Password = args[1],
		Filename = args[2],
	};
	 
	switch(args[3])
	{
		case "live":
			param.Prod = true;
			break;
		case "test":
			param.Prod = false;
			break;
		default:
			Console.WriteLine("Submission type should be specified as 'test' or 'live'. Example:");
			PrintArgs(TestInput.TestParam); 
			return -1;
			break;
	}
}
else
{
	param = TestInput.TestParam;
	Console.WriteLine("There should be exactly " + expectedArgCount + " arguments. Example:");
	PrintArgs(param);
	return -1;
}
PrintArgs(param);

bool result = await new Submitter(new InstanceReader(), new InstancePoster()).Submit(param);
if (result)
{
	Console.WriteLine("Completed");
	Console.WriteLine("Live Submit Call Report");
	Console.WriteLine("Your Call Report has been submitted to the CDR.");
	Console.WriteLine("This DOES NOT mean it has been accepted by the CDR.");
	Console.WriteLine("You must wait for a CDR email that will indicate whether your Call Report has been accepted or rejected.");
	Console.WriteLine("The e-mail message will be sent to the following:");
	Console.WriteLine("    Authorized Officer Signing the Reports: TEXTC490 <TEXTC492>");
	Console.WriteLine("    Other Person to Whom Questions about the");
	Console.WriteLine("       Reports Should be Directed: TEXTC495 <TEXT4086>");
}
else
{
	Console.WriteLine("Error contacting CDR: Process aborted");
	Console.WriteLine("Live Submit Call Report Error B2");
	SuggestWebsiteOpen();
}
return 0;

void SuggestWebsiteOpen()
{
	Console.WriteLine("Live CDR Submit Error");
	Console.WriteLine("Would you like to exit EasyCALL and try submitting your Call Report directly to the CDR website? (Y/N)");

	string yn = string.Empty;
	Func<bool> isaYes = () => string.Compare("Y", yn?.Trim(), ignoreCase: true) == 0;
	Func<bool> isaNo = () => string.Compare("N", yn?.Trim(), ignoreCase: true) == 0;

	for (yn = Console.ReadLine(); !isaNo(); yn = Console.ReadLine())
	{
		if (isaYes())
		{
			Process.Start(new ProcessStartInfo("https://cdr.ffiec.gov/CDR/Login.aspx") { UseShellExecute = true });
			break;
		}
	}
}

void PrintArgs(SubmitParam args)
{
	string exeName = Process.GetCurrentProcess().MainModule.ModuleName;
	Console.Write(exeName + " ");
	Console.Write(args.Username + " ");
	Console.Write(args.Password + " "); 
	Console.Write(args.Filename + " ");
	Console.Write(args.Prod?"live": "test");
	Console.WriteLine(string.Empty);
	Console.WriteLine($"{exeName} Username Password NumberOfPeriods XmlFileName");
}
return 0;