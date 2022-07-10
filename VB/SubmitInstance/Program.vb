Module Program
	Sub Main(args As String())

		Const dateFormat As String = "yyyy-MM-dd"
		Const expectedArgCount As Integer = 4

Dim param As SubmitParam

If args.Length = 0 Then
				param = TestInput.TestParam
			ElseIf args.Length = expectedArgCount Then
			param = New()
	{
		Username = args[0],
		Password = args[1],
		Filename = args[2],
	};

''' 
                Select Case args(3)
					Case "live"
						param.Prod = True
					Case "test"
						param.Prod = False
					Case Else
						Console.WriteLine("Submission type should be specified as 'test' or 'live'. Example:")
						PrintArgs(TestInput.TestParam)
						Return -1
				End Select
			Else
				param = TestInput.TestParam
				Console.WriteLine("There should be exactly " & expectedArgCount & " arguments. Example:")
				PrintArgs(param)
				Return -1
			End If

			PrintArgs(param)
			Dim cdrSettings = New Settings()
			Console.WriteLine("Reading settings from CDR.ini")

			If Not File.Exists("CDR.ini") Then
				Console.WriteLine("CDR.ini cannot be found")
				Return -1
			End If

			Using file = New FileStream("CDR.ini", FileMode.Open)
				cdrSettings.Load(file)
			End Using

			Dim result As Boolean = Await New Submitter(New InstanceReader(), New InstancePoster(), cdrSettings, New RequestBuilder()).Submit(param)

            If result Then
				Console.WriteLine("Completed")
				Console.WriteLine("Live Submit Call Report")
				Console.WriteLine("Your Call Report has been submitted to the CDR.")
				Console.WriteLine("This DOES NOT mean it has been accepted by the CDR.")
				Console.WriteLine("You must wait for a CDR email that will indicate whether your Call Report has been accepted or rejected.")
				Console.WriteLine("The e-mail message will be sent to the following:")
				Console.WriteLine("    Authorized Officer Signing the Reports: TEXTC490 <TEXTC492>")
				Console.WriteLine("    Other Person to Whom Questions about the")
				Console.WriteLine("       Reports Should be Directed: TEXTC495 <TEXT4086>")
			Else
				Console.WriteLine("Error contacting CDR: Process aborted")
				Console.WriteLine("Live Submit Call Report Error B2")
				SuggestWebsiteOpen()
			End If

			Return 0
                        ''' Cannot convert LocalFunctionStatementSyntax, CONVERSION ERROR: Conversion for LocalFunctionStatement not implemented, please report this issue in 'void SuggestWebsiteOpen()
{...' at character 2109
'''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.DefaultVisit(SyntaxNode node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.LocalFunctionStatementSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)
''' 
''' Input: 
''' 
Void SuggestWebsiteOpen()
{
	Console.WriteLine("Live CDR Submit Error");
	Console.WriteLine("Would you like to exit EasyCALL and try submitting your Call Report directly to the CDR website? (Y/N)");

	String yn = String.Empty;
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

''' 
                        ''' Cannot convert LocalFunctionStatementSyntax, CONVERSION ERROR: Conversion for LocalFunctionStatement not implemented, please report this issue in 'void PrintArgs(SubmitParam ...' at character 2718
'''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.DefaultVisit(SyntaxNode node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.LocalFunctionStatementSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)
''' 
''' Input: 
''' 
void PrintArgs(SubmitParam args)
{
	string exeName = Process.GetCurrentProcess().MainModule.ModuleName;
	Console.Write(exeName + " ");
	Console.Write(args.Username + " ");
	Console.Write(new string('*', args.Password.Length) + " ");
	Console.Write(args.Filename + " ");
	Console.Write(args.Prod ? "live" : "test");
	Console.WriteLine(string.Empty);
	Console.WriteLine($"{exeName} Username Password NumberOfPeriods XmlFileName");
}

''' 
            Return 0
 

		End Sub
End Module
