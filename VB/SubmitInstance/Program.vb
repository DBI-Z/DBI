Imports System.IO

Module Program
	Function Main(args As String()) As Integer
		Const expectedArgCount As Integer = 4
		Dim param As SubmitParam

		If args.Length = 0 Then
			param = TestInput.TestParam
		ElseIf args.Length = expectedArgCount Then
			param = New SubmitParam
			param.Username = args(0)
			param.Password = args(1)
			param.Filename = args(2)

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
		End If

		Using file = New FileStream("CDR.ini", FileMode.Open)
			cdrSettings.Load(file)
		End Using

		Dim result As Boolean = New Submitter(New InstanceReader(), New InstancePoster(), cdrSettings, New RequestBuilder()).Submit(param).Result

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
			Return 0
		Else
			Console.WriteLine("Error contacting CDR: Process aborted")
			Console.WriteLine("Live Submit Call Report Error B2")
			SuggestWebsiteOpen()
			Return -1
		End If
	End Function

	Sub SuggestWebsiteOpen()
		Console.WriteLine("Live CDR Submit Error")
		Console.WriteLine("Would you like to exit EasyCALL and try submitting your Call Report directly to the CDR website? (Y/N)")
		Dim yn As String = String.Empty
		Dim isaYes As Func(Of Boolean) = Function() String.Compare("Y", yn?.Trim(), ignoreCase:=True) = 0
		Dim isaNo As Func(Of Boolean) = Function() String.Compare("N", yn?.Trim(), ignoreCase:=True) = 0

	End Sub

	Private Sub PrintArgs(ByVal args As SubmitParam)
		Dim exeName As String = Process.GetCurrentProcess().MainModule.ModuleName
		Console.Write(exeName & " ")
		Console.Write(args.Username & " ")
		Console.Write(New String("*"c, args.Password.Length) & " ")
		Console.Write(args.Filename & " ")
		Console.Write(If(args.Prod, "live", "test"))
		Console.WriteLine(String.Empty)
		Console.WriteLine($"{exeName} Username Password NumberOfPeriods XmlFileName")
	End Sub
End Module