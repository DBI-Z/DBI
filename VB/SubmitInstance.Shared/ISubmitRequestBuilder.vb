﻿Public Interface ISubmitRequestBuilder
	Function Build(ByVal param As SubmitParam, ByVal submitNamespace As String, ByVal content As String) As XDocument
End Interface