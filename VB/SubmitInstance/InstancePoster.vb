Imports SimpleSOAPClient
Imports SimpleSOAPClient.Helpers
Imports SimpleSOAPClient.Models

Friend Class InstancePoster
	Implements IInstancePoster

	Public Async Function Post(ByVal server As String, ByVal action As String, ByVal body As XDocument) As Task(Of XDocument) Implements IInstancePoster.Post
		Dim client As SoapClient = SoapClient.Prepare()
		Dim requestEnvelope As SoapEnvelope = SoapEnvelope.Prepare().Body(Of XElement)(body.Root)
		Dim responseEnvelope As SoapEnvelope = Await client.SendAsync(server, action, requestEnvelope)
		Return New XDocument(responseEnvelope.Body.Value)
	End Function
End Class