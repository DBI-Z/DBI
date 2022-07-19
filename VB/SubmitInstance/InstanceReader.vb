Imports System.IO

Friend Class InstanceReader
	Implements IInstanceReader

	Public Function Read(xmlFileName As String) As String Implements IInstanceReader.Read
		If File.Exists(xmlFileName) Then
			Dim r As StreamReader = New StreamReader(xmlFileName)
			Return r.ReadToEnd()
		Else
			Return String.Empty
		End If
	End Function
End Class
