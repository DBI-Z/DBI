Imports System.Xml.Linq

Namespace GetInstance
	Public Interface IExtractor
		Function Extract(ByVal xbrl As XDocument) As List(Of WriteFormat)
	End Interface
End Namespace
