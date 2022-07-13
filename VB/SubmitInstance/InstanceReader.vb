Imports System.IO

Friend Class InstanceReader
	Implements IInstanceReader

	Public Function Read(ByVal xmlFileName As String) As String Implements IInstanceReader.Read
		If File.Exists(xmlFileName) Then
			Dim r As StreamReader = New StreamReader(xmlFileName)
			Return r.ReadToEnd()
		Else
			Console.WriteLine($"File {xmlFileName} cannot be found.")
			Return String.Empty
		End If
	End Function
End Class
