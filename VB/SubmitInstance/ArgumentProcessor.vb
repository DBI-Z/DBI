Imports System.IO

Friend Class ArgumentProcessor
	Private args As String()
	Private displayer As IDisplayer

	Public Sub New(ByVal args As String(), ByVal displayer As IDisplayer)
		Me.args = args
		Me.displayer = displayer
	End Sub

	Public Function GetParam() As SubmitParam
		Const expectedArgCount As Integer = 4
		Dim param As SubmitParam

		If args Is Nothing Then
			param = Nothing
		ElseIf args.Length = 1 Then
			param = Nothing
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
					displayer.WriteLine("Submission type should be specified as 'test' or 'live'. Example:")
					PrintArgs(TestParam)
					param = Nothing
			End Select
		Else
			param = Nothing
			displayer.WriteLine("There should be exactly " & expectedArgCount & " arguments. Example:")
			PrintArgs(TestParam)
		End If
		Return param
	End Function

	Function ReadIni() As ISettings
		Dim cdrSettings = New Settings()
		displayer.WriteLine("Reading settings from CDR.ini")

		If Not File.Exists("CDR.ini") Then
			displayer.WriteLine("CDR.ini cannot be found")
		End If

		Using file = New FileStream("CDR.ini", FileMode.Open)
			cdrSettings.Load(file)
		End Using

		Return cdrSettings
	End Function

	Private Sub PrintArgs(args As SubmitParam)
		Dim exeName As String = Process.GetCurrentProcess().MainModule.ModuleName
		displayer.Write(exeName & " ")
		displayer.Write(args.Username & " ")
		displayer.Write(New String("*"c, args.Password.Length) & " ")
		displayer.Write(args.Filename & " ")
		displayer.Write(If(args.Prod, "live", "test"))
		displayer.WriteLine(String.Empty)
		displayer.WriteLine($"{exeName} Username Password NumberOfPeriods XmlFileName")
	End Sub

	Public Shared ReadOnly Property TestParam As SubmitParam
		Get
			Return New SubmitParam With {
								.Username = "DBIFINAN2",
								.Password = "TestingNew12",
								.Filename = "ffiec_141958_2022-03-31.xml",
								.Prod = True
				}
		End Get
	End Property

End Class