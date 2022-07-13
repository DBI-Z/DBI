Imports System.Xml.Linq

Public Interface IInstancePoster
	Function Post(ByVal server As String, ByVal action As String, ByVal param As XDocument) As Task(Of XDocument)
End Interface