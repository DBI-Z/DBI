Code Converter Logo
C#
VB.NET
Using SimpleSOAPClient;
Using SimpleSOAPClient.Helpers;
Using SimpleSOAPClient.Models;
Using System.Xml.Linq;

Namespace SubmitInstance
{
	internal Class InstancePoster :  IInstancePoster
	{
		Public Async Task<XDocument> Post(String server, String action, XDocument body)
		{
			SoapClient client = SoapClient.Prepare();
			SoapEnvelope requestEnvelope = SoapEnvelope.Prepare().Body(body.Root);
			SoapEnvelope responseEnvelope = await client.SendAsync(server, action, requestEnvelope);
			Return New XDocument(responseEnvelope.Body.Value);
		}
	}
}

 
Imports SimpleSOAPClient
		Imports SimpleSOAPClient.Helpers
		Imports SimpleSOAPClient.Models
		Imports System.Xml.Linq

Namespace SubmitInstance
		Friend Class InstancePoster
			Inherits IInstancePoster

			Public Async Function Post(ByVal server As String, ByVal action As String, ByVal body As XDocument) As Task(Of XDocument)
				Dim client As SoapClient = SoapClient.Prepare()
				Dim requestEnvelope As SoapEnvelope = SoapEnvelope.Prepare().Body(body.Root)
				Dim responseEnvelope As SoapEnvelope = Await client.SendAsync(server, action, requestEnvelope)
				Return New XDocument(responseEnvelope.Body.Value)
			End Function
		End Class
	End Namespace

Progress Telerik
Copyright © 2022, Progress Software Corporation And/Or its subsidiaries Or affiliates. All Rights Reserved.

Progress, Telerik, And certain product names used herein are trademarks Or registered trademarks of Progress Software Corporation And/Or one of its subsidiaries Or affiliates in the U.S. And/Or other countries. See Trademarks for appropriate markings.

About Terms Of Use Privacy Policy