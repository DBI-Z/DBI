Imports System.Xml.Linq

Namespace GetInstance
	Public Interface IInstanceDownloader
		Function Download(ByVal param As GetInstanceRequest) As Task(Of XDocument)
	End Interface
End Namespace