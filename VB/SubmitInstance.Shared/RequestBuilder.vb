Public Class RequestBuilder
	Implements ISubmitRequestBuilder

	Public Function Build(ByVal param As SubmitParam, ByVal submitNamespace As String, ByVal content As String) As XDocument Implements ISubmitRequestBuilder.Build
		Dim submitElementName As String = If(param.Prod, Settings.SubmitRequestProd, Settings.SubmitRequestTest)
		Dim envelopeBody As XDocument = New XDocument(New XElement(XName.[Get](submitElementName, submitNamespace), New XElement() {New XElement(XName.[Get]("userName", submitNamespace), param.Username), New XElement(XName.[Get]("password", submitNamespace), param.Password), New XElement(XName.[Get]("instanceData", submitNamespace), content)}))
		Return envelopeBody
	End Function
End Class