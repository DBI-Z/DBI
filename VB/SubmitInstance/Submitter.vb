﻿Imports System.Text
Imports System.Xml
Imports System.Xml.Linq
Imports System.Xml.Serialization
Imports System.Xml.XPath

Namespace SubmitInstance
	Public Class Submitter
		Private reader As IInstanceReader
		Private poster As IInstancePoster
		Private settings As ISettings
		Private builder As ISubmitRequestBuilder

		Public Sub New(ByVal reader As IInstanceReader, ByVal poster As IInstancePoster, ByVal settings As ISettings, ByVal builder As ISubmitRequestBuilder)
			Me.reader = reader
			Me.poster = poster
			Me.settings = settings
			Me.builder = builder
		End Sub

		Public Async Function Submit(ByVal param As SubmitParam) As Task(Of Boolean)
			Console.WriteLine("Submitting Call Report to the CDR")
			Console.WriteLine("1. Logging on to CDR")
			Dim responseBody As XDocument = Nothing

			Try
				Dim url As String = If(param.Prod, settings.Url, settings.UrlT)
				Dim action As String = If(param.Prod, settings.SubmitAction, settings.TestSubmitAction)
				Console.WriteLine("2. Submitting Call Report: ")
				Console.WriteLine(url)
				Console.WriteLine(action)
				Dim fileContents As String = reader.Read(param.Filename)

				If String.IsNullOrWhiteSpace(fileContents) Then
					Return False
				End If

				Dim submitBody As XDocument = builder.Build(param, settings.NS, fileContents)
				responseBody = Await poster.Post(url, action, submitBody)
				Console.WriteLine(responseBody.ToString())
				Dim response = New Cdr().ExtractSubmitResponse(responseBody, param.Prod)
				Dim IsSuccessfulGetInstance As Boolean = response.ReturnCode = 0

				If IsSuccessfulGetInstance Then
					Return True
				Else
					Console.WriteLine("Code: " & response.ReturnCode)
					Console.WriteLine("Message: " & response.ReturnMessage)
					Return False
				End If

			Catch ex As IOException
				Console.WriteLine(ex.Message)
				Console.WriteLine($"Error while reading file {param.Filename}")
				Return False
			Catch ex As HttpRequestException
				Console.WriteLine(ex.Message)
				Dim errmsg As String = "Unable to connect. Some firewalls require altering permissions to allow EasyCall Report to communicate with the Central Data Repository.  Your information technology department should be made aware that this communication uses HTTPS via port 443." & "Also, the FFIEC CDR system now only supports TLS 1.2;  please insure your operating system supports said."
				Console.WriteLine(PrepareMsg(errmsg))
				Return False
			Catch ex As Exception
				Console.WriteLine(ex.Message)
				Dim interpretedErrorMessage As String = PrepareMsg(If(responseBody?.ToString(), String.Empty))
				Console.WriteLine(interpretedErrorMessage)
				Return False
			End Try
		End Function

		Private Function PrepareMsg(ByVal subText As String) As String
			Const defaultErrorCode As Integer = 6
                        ''' Cannot convert LocalDeclarationStatementSyntax, CONVERSION ERROR: Conversion for TupleType not implemented, please report this issue in '(string phrase, int code)' at character 2666
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.DefaultVisit(SyntaxNode node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitTupleType(TupleTypeSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.TupleTypeSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitTupleType(TupleTypeSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.TupleTypeSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitArrayType(ArrayTypeSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.ArrayTypeSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitArrayType(ArrayTypeSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.ArrayTypeSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at ICSharpCode.CodeConverter.VB.CommonConversions.RemodelVariableDeclaration(VariableDeclarationSyntax declaration)
'''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)
''' 
''' Input: 
''' 			(string phrase, int code)[] errorPhrases = new[]
			{
						("not authorized",1 ),
						("not match",2 ),
						("Access denied",3 ),
						("valid XML",4 ),
						("ID_RSSD",5 )
				};

''' 
                        ''' Cannot convert LocalDeclarationStatementSyntax, CONVERSION ERROR: Conversion for TupleType not implemented, please report this issue in '()' at character 2879
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.DefaultVisit(SyntaxNode node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitTupleType(TupleTypeSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.TupleTypeSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitTupleType(TupleTypeSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.TupleTypeSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.ObjectCreationExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.ObjectCreationExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at ICSharpCode.CodeConverter.VB.CommonConversions.ConvertTopLevelExpression(ExpressionSyntax topLevelExpression)
'''    at ICSharpCode.CodeConverter.VB.CommonConversions.RemodelVariableDeclaration(VariableDeclarationSyntax declaration)
'''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)
''' 
''' Input: 
''' 
			StringBuilder sb = New();

''' 
            Dim errorCode As Integer = defaultErrorCode

			For i As Integer = 0 To errorPhrases.Length - 1
				If subText.IndexOf(errorPhrases(i).phrase) > 0 Then errorCode = i
			Next

			sb.AppendLine("A problem was encountered while attempting to submit your Call Report to the CDR.")

			Select Case errorCode
				Case 1
					sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.")
				Case 4
					sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.")
				Case 5, defaultErrorCode
					sb.AppendJoin("Error", errorCode, subText)
				Case 2
					sb.AppendLine("Invalid User Name or Password, or User is locked.")
					sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.")
				Case 3
					sb.AppendLine("Invalid User Name or Password, User is locked, or mandatory training must be completed.")
					sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.")
			End Select

			sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.")
			Return sb.ToString()
		End Function
	End Class

	<XmlRoot("SubmitInstanceData", [Namespace]:="http://ffiec.gov/cdr/services/")>
	Public Class SubmitInstanceDataRequest
		<XmlElement("userName")>
		Public Property UserName As String
		<XmlElement("password")>
		Public Property Password As String
		<XmlElement("instanceData")>
		Public Property InstanceData As String
	End Class
End Namespace