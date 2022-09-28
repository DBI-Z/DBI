Imports System.IO

Friend Class InstanceReader
	Implements IInstanceReader

	Public Function Read(xmlFileName As String) As String Implements IInstanceReader.Read
		Dim absolutePath As String
		If Path.IsPathRooted(xmlFileName) Then
			absolutePath = xmlFileName
		Else
			absolutePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFileName)
		End If

		If File.Exists(absolutePath) Then
			Dim r As StreamReader = New StreamReader(absolutePath)
			Return r.ReadToEnd()
		Else
			Return String.Empty
		End If
	End Function
End Class
