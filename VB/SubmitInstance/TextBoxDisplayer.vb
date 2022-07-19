Friend Class TextBoxDisplayer
	Implements IDisplayer

	Public textbox As TextBox

	Public Sub New(ByVal textbox As TextBox)
		Me.textbox = textbox
	End Sub

	Public Sub Write(ByVal text As String) Implements IDisplayer.Write
		If Not textbox.Disposing AndAlso Not textbox.IsDisposed Then textbox.AppendText(text)
	End Sub

	Public Sub WriteLine(ByVal text As String) Implements IDisplayer.WriteLine
		If Not textbox.Disposing AndAlso Not textbox.IsDisposed Then textbox.AppendText(text & vbCrLf)
	End Sub
End Class