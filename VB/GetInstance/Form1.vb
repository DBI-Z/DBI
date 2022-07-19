Public Class Form1

	Private cmdLineArgs As String()
	Private fromCmdLineArgs As Boolean

	Public Sub New()
		InitializeComponent()
		displayer = New TextBoxDisplayer(txtLog)
		Dim args As String() = Environment.GetCommandLineArgs
		If args IsNot Nothing Then
			If args.Length > 1 Then
				cmdLineArgs = New String(args.Length - 2) {}
				Array.Copy(args, 1, cmdLineArgs, 0, cmdLineArgs.Length)
			End If
		End If
	End Sub

	Private displayer As IDisplayer

	Private Sub Form1_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
		displayer.WriteLine("Welcome to EasyCall Report's Live CDR Update")
		Dim processor As ArgumentProcessor = New ArgumentProcessor(cmdLineArgs, displayer)
		Dim request As GetInstanceRequest = processor.GetRequest()

		If request Is Nothing Then
			Fill(ArgumentProcessor.TestRequest)
		Else
			Fill(request)
			GetCdr(request)
		End If
	End Sub

	Private Sub Fill(ByVal request As GetInstanceRequest)
		txtUsername.Text = request.Username
		txtPassword.Text = request.Password
		txtRssd.Text = request.IdRssd
		txtPeriods.Text = request.NumberOfPriorPeriods.ToString()
		txtDate.Text = request.ReportingPeriodEndDate.ToString(GetInstanceRequest.DateFormat)
	End Sub

	Private Function GetRequestFromUI() As GetInstanceRequest
		Dim nonEmptyArgs As List(Of String) = New List(Of String)

		Dim uiArgs As String() = New String() {txtUsername.Text, txtPassword.Text, "call", txtRssd.Text, txtPeriods.Text, txtDate.Text}

		For Each arg As String In uiArgs
			If Not String.IsNullOrWhiteSpace(arg) Then nonEmptyArgs.Add(arg)
		Next

		Return New ArgumentProcessor(nonEmptyArgs.ToArray(), displayer).GetRequest()
	End Function

	Private Async Function GetCdr(ByVal request As GetInstanceRequest) As Task
		displayer.WriteLine("Updating history with prior quarter data from the CDR...")
		displayer.WriteLine("1. Logging on to CDR: ")
		Await New InstanceGetter(New Prior2Downloader(displayer), New CsvWriter(displayer), New Extractor(displayer), displayer).[Get](request)
	End Function

	Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		lblFormat.Text = GetInstanceRequest.DateFormat
	End Sub

	Private Sub btnGetCdr_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetCdr.Click
		displayer.WriteLine(String.Empty)
		displayer.WriteLine("-----------------")
		Dim uiRequest As GetInstanceRequest = GetRequestFromUI()
		If uiRequest IsNot Nothing Then GetCdr(uiRequest)
	End Sub

	Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
		Close()
	End Sub
End Class