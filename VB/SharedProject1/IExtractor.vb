Public Interface IExtractor
	Function Extract(ByVal xbrl As XDocument) As List(Of WriteFormat)
End Interface