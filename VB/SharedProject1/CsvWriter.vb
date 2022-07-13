Imports System.IO

Public Class CsvWriter
		Implements IInstanceWriter

		Private displayer As IDisplayer

		Public Sub New(ByVal displayer As IDisplayer)
			Me.displayer = displayer
		End Sub

		Public Sub Write(records As IEnumerable(Of WriteFormat), instance As Stream) Implements IInstanceWriter.Write
			Dim sw As StreamWriter = New StreamWriter(instance)
			For Each writeFormat In records
				Dim csv As String = String.Join(",", Quoted(writeFormat.MtMdrm), Quoted(writeFormat.MtContextRef), Quoted(writeFormat.MtUnitRef), Quoted(writeFormat.MtDecimals), Quoted(writeFormat.MtData))
				displayer.WriteLine(csv)
				Debug.WriteLine(csv)
				sw.WriteLine(csv)
			Next
			sw.Dispose()
			instance.Dispose()
		End Sub

		Private Function Quoted(ByVal text As String) As String
			Return """"c & text & """"c
		End Function
	End Class
