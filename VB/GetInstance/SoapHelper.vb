Imports SimpleSOAPClient
Imports SimpleSOAPClient.Handlers
Imports SimpleSOAPClient.Helpers

Friend Class SoapHelper
	Private soapClient As SoapClient = Nothing

	Public Function GetInstance() As SoapClient
		Return If(soapClient Is Nothing, CSharpImpl.__Assign(soapClient, Prepare()), soapClient)
	End Function

	Private Function Prepare() As SoapClient
		Dim a = SoapClient.Prepare().WithHandler(New DelegatingSoapHandler With {
							.OnSoapEnvelopeRequestAsyncAction = Async Function(c, d, cancellationToken)
																																											End Function,
							.OnHttpRequestAsyncAction = Async Function(soapClient, d, cancellationToken)
																																			End Function,
							.OnHttpResponseAsyncAction = Async Function(soapClient, d, cancellationToken)
																																				End Function,
							.OnSoapEnvelopeResponseAsyncAction = Async Function(soapClient, d, cancellationToken)
																																												End Function
			})
		Return a
	End Function

	Private Class CSharpImpl
		<Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
		Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
			target = value
			Return value
		End Function
	End Class
End Class