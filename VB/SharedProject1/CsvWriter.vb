Imports System.IO

Namespace GetInstance
	Public Class CsvWriter
		Implements IInstanceWriter

		Private displayer As IDisplayer

		Public Sub New(ByVal displayer As IDisplayer)
			Me.displayer = displayer
		End Sub

		Public Sub IInstanceWriter_Write(records As IEnumerable(Of WriteFormat), instanceFile As Stream) Implements IInstanceWriter.Write
			Dim sw As StreamWriter = New StreamWriter(instanceFile)
			For Each writeFormat In records
				Dim csv As String = String.Join(",", Quoted(writeFormat.MtMdrm), Quoted(writeFormat.MtContextRef), Quoted(writeFormat.MtUnitRef), Quoted(writeFormat.MtDecimals), Quoted(writeFormat.MtData))
				displayer.WriteLine(csv)
				Debug.WriteLine(csv)
				sw.WriteLine(csv)
			Next
		End Sub

		Private Function Quoted(ByVal text As String) As String
			Return """"c & text & """"c
		End Function
	End Class
End Namespace