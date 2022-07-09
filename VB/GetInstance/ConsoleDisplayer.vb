Namespace GetInstance
	Friend Class ConsoleDisplayer
		Implements IDisplayer

		Public Sub Write(ByVal text As String) Implements IDisplayer.Write
			Console.Write(text)
		End Sub

		Public Sub WriteLine(ByVal text As String) Implements IDisplayer.WriteLine
			Console.WriteLine(text)
		End Sub
	End Class
End Namespace
