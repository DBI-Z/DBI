Public Class Form1
	Private argsWithoutUserPass As String()
	Private displayer As IDisplayer
	Private cdrSettings As ISettings
	Private processor As ArgumentProcessor

	Public Sub New()
		InitializeComponent()
		displayer = New TextBoxDisplayer(txtLog)
		Dim args As String() = Environment.GetCommandLineArgs
		If args IsNot Nothing Then
			If args.Length > 1 Then
				argsWithoutUserPass = New String(args.Length) {}
				Array.Copy(args, 1, argsWithoutUserPass, 2, argsWithoutUserPass.Length - 2)
			End If
		End If
	End Sub

	Public ReadOnly Property LogDisplayTextbox As TextBox
		Get
			Return txtLog
		End Get
	End Property

	Private Sub Form1_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
		displayer.WriteLine("Welcome to EasyCall Report's Live CDR Call Report Submisson")
		processor = New ArgumentProcessor(argsWithoutUserPass, displayer)

		Dim request As SubmitParam = processor.GetParam()

		If request Is Nothing Then
			Fill(ArgumentProcessor.TestParam)
		Else
			Fill(request)
		End If
	End Sub

	Async Sub Submit()
		cdrSettings = processor.ReadIni()
		Dim s As Submitter = New Submitter(New InstanceReader(), New InstancePoster(), cdrSettings, New RequestBuilder(), displayer)
		displayer.WriteLine(String.Empty)
		displayer.WriteLine("-----------------")
		Dim uiRequest As SubmitParam = GetRequestFromUI()

		If uiRequest IsNot Nothing Then
			Dim result As Boolean = Await s.Submit(uiRequest)
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
			End If
		End If
	End Sub

	Private Sub Fill(ByVal request As SubmitParam)
		txtUsername.Text = request.Username
		txtPassword.Text = request.Password
		txtFile.Text = request.Filename
		chcTest.Checked = Not request.Prod
	End Sub

	Private Function GetRequestFromUI() As SubmitParam
		Dim nonEmptyArgs As List(Of String) = New List(Of String)

		Dim uiArgs As String() = New String() {txtUsername.Text, txtPassword.Text, txtFile.Text, If(chcTest.Checked, "test", "live")}

		Return New ArgumentProcessor(uiArgs.ToArray(), displayer).GetParam()
	End Function

	Dim settings As ISettings

	Private Sub btnSubmitCdr_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
		Submit()
	End Sub

	Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

	End Sub

	Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
		Close()
	End Sub
End Class