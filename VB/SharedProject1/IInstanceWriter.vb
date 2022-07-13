Imports System.IO

Public Interface IInstanceWriter
		Sub Write(ByVal records As IEnumerable(Of WriteFormat), ByVal instanceFile As Stream)
	End Interface 