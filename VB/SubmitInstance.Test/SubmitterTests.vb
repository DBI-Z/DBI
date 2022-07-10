Imports Xunit
Imports System.Xml.Linq

Namespace SubmitInstance.Test
	Public Class SubmitterTests
		<Fact>
		Public Sub SubmitRequest_BuildBody_RequestBodyIsTest()
			''' Cannot convert LocalDeclarationStatementSyntax, CONVERSION ERROR: Conversion for TupleType not implemented, please report this issue in '()' at character 211
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
			''' 			//Arrange
			RequestBuilder rb = New();

''' 
                        ''' Cannot convert LocalDeclarationStatementSyntax, CONVERSION ERROR: Conversion for TupleType not implemented, please report this issue in '()' at character 247
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
''' 			SubmitParam submitParam = new() { Prod = false };

''' 
            Dim body As XDocument = rb.Build(submitParam, String.Empty, String.Empty)
			Dim bodyProse As String = body.ToString()
			Assert.Contains(Settings.SubmitRequestTest, bodyProse)
		End Sub

		<Fact>
		Public Sub SubmitRequest_BuildBody_RequestBodyIsProd()
			''' Cannot convert LocalDeclarationStatementSyntax, CONVERSION ERROR: Conversion for TupleType not implemented, please report this issue in '()' at character 575
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
			''' 			//Arrange
			RequestBuilder rb = New();

''' 
                        ''' Cannot convert LocalDeclarationStatementSyntax, CONVERSION ERROR: Conversion for TupleType not implemented, please report this issue in '()' at character 611
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
''' 			SubmitParam submitParam = new() { Prod = true };

''' 
            Dim body As XDocument = rb.Build(submitParam, String.Empty, String.Empty)
			Dim bodyProse As String = body.ToString()
			Assert.Contains(Settings.SubmitRequestProd, bodyProse)
		End Sub

		<Fact>
		Public Sub SubmitRequest_BuildBody_UserPassIncluded()
			Const username As String = "user1234"
			Const password As String = "pass1234"
			''' Cannot convert LocalDeclarationStatementSyntax, CONVERSION ERROR: Conversion for TupleType not implemented, please report this issue in '()' at character 1015
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
			''' 			RequestBuilder rb = new();

			''' 
			''' Cannot convert LocalDeclarationStatementSyntax, CONVERSION ERROR: Conversion for TupleType not implemented, please report this issue in '()' at character 1051
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
			''' 			SubmitParam submitParam = new() { Prod = true, Username = username, Password = password };

			''' 
			Dim body As XDocument = rb.Build(submitParam, String.Empty, String.Empty)
			Dim bodyProse As String = body.ToString()
			Assert.Contains(username, bodyProse)
			Assert.Contains(password, bodyProse)
		End Sub

		<Fact>
		Public Sub SubmitRequest_BuildBody_ContentIncluded()
			Const checkValue As String = "4a4b-5188-aafd-dd31"
			Const document As String = $"<xbrl>{checkValue}</xbrl>"
			''' Cannot convert LocalDeclarationStatementSyntax, CONVERSION ERROR: Conversion for TupleType not implemented, please report this issue in '()' at character 1550
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
			''' 			RequestBuilder rb = new();

			''' 
			''' Cannot convert LocalDeclarationStatementSyntax, CONVERSION ERROR: Conversion for TupleType not implemented, please report this issue in '()' at character 1586
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
			''' 			SubmitParam submitParam = new() { Prod = true };

			''' 
			Dim body As XDocument = rb.Build(submitParam, String.Empty, document)
			Dim bodyProse As String = body.ToString()
			Assert.Contains(checkValue, bodyProse)
		End Sub

		<Fact>
		Public Sub Submit_Response_ReadErrorCode()
			Dim responseTemplate As String = "<SubmitTestInstanceDataResponse xmlns=""http://ffiec.gov/cdr/services/"">
																																					<SubmitTestInstanceDataResult>
																																						<CdrServiceSubmitInstanceData xmlns=""http://Cdr.Business.Workflow.Schemas.CdrServiceSubmitInstanceData"">
																																							<Outputs xmlns="""">
																																								<ErrorCode>100</ErrorCode>
																																								<ErrorMessage>ErrorMessage100</ErrorMessage>
																																							</Outputs>
																																						</CdrServiceSubmitInstanceData>
																																					</SubmitTestInstanceDataResult>
																																			</SubmitTestInstanceDataResponse>"
			Dim responseXml As XDocument

			Using responseStream = New StringReader(responseTemplate)
				responseXml = XDocument.Load(responseStream)
			End Using

			Dim response = New Cdr().ExtractSubmitResponse(responseXml, False)
			Assert.Equal(100, response.ReturnCode)
			Assert.Equal("ErrorMessage100", response.ReturnMessage)
		End Sub

		<Fact>
		Public Sub Submit_Response_ReadReturnCode()
			Dim responseTemplate As String = "<SubmitTestInstanceDataResponse xmlns=""http://ffiec.gov/cdr/services/"">
																																					<SubmitTestInstanceDataResult>
																																						<CdrServiceSubmitInstanceData xmlns=""http://Cdr.Business.Workflow.Schemas.CdrServiceSubmitInstanceData"">
																																							<Outputs xmlns="""">
																																								<ReturnCode>100</ReturnCode>
																																								<ReturnMessage>ReturnMessage100</ReturnMessage>
																																							</Outputs>
																																						</CdrServiceSubmitInstanceData>
																																					</SubmitTestInstanceDataResult>
																																			</SubmitTestInstanceDataResponse>"
			Dim responseXml As XDocument

			Using responseStream = New StringReader(responseTemplate)
				responseXml = XDocument.Load(responseStream)
			End Using

			Dim response = New Cdr().ExtractSubmitResponse(responseXml, False)
			Assert.Equal(100, response.ReturnCode)
			Assert.Equal("ReturnMessage100", response.ReturnMessage)
		End Sub

		<Fact>
		Public Sub Submit_Response_NullReturnCode()
			Dim responseTemplate As String = "<?xml version=""1.0"" encoding=""utf-8""?>
																															<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
																																	<soap:Body>
																																			<SubmitTestInstanceDataResponse xmlns=""http://ffiec.gov/cdr/services/"">
																																					<SubmitTestInstanceDataResult>string</SubmitTestInstanceDataResult>
																																			</SubmitTestInstanceDataResponse>
																																	</soap:Body>
																															</soap:Envelope>"
			Dim responseXml As XDocument

			Using responseStream = New StringReader(responseTemplate)
				responseXml = XDocument.Load(responseStream)
			End Using

			Dim response = New Cdr().ExtractSubmitResponse(responseXml, False)
			Assert.Null(response.ReturnCode)
			Assert.Null(response.ReturnMessage)
		End Sub

		<Fact>
        Public Sub Submit_Settings_ParsedCorrectly()
            Dim sampleContents As String = "GetAction = http://ffiec.gov/cdr/services/GetInstanceData
																													URL = https://c1-test-cdr-ffiec.com/FFIEC/services/CdrService.asmx?WSDL
																													URLT = https://c1-test-cdr-ffiec.com/FFIEC/services/CdrService.asmx?WSDL
																													NS = http://ffiec.gov/cdr/services/
																													SubmitAction = http://ffiec.gov/cdr/services/SubmitInstanceData
																													TestSubmitAction = http://ffiec.gov/cdr/services/SubmitTestInstanceData"
                        ''' Cannot convert LocalDeclarationStatementSyntax, CONVERSION ERROR: Conversion for TupleType not implemented, please report this issue in '(sampleContents.Length)' at character 5853
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
''' 			MemoryStream ms = new(sampleContents.Length);

''' 
                        ''' Cannot convert LocalDeclarationStatementSyntax, CONVERSION ERROR: Conversion for TupleType not implemented, please report this issue in '(ms)' at character 5902
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
''' 			StreamWriter sw = new(ms);

''' 
            sw.Write(sampleContents)
            sw.Flush()
            ms.Seek(0, SeekOrigin.Begin)
                        ''' Cannot convert LocalDeclarationStatementSyntax, CONVERSION ERROR: Conversion for TupleType not implemented, please report this issue in '()' at character 6021
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
			//Act
			Settings settings = new();

''' 
            settings.Load(ms)
            sw.Dispose()
            ms.Dispose()
            Assert.Equal("http://ffiec.gov/cdr/services/GetInstanceData", settings.GetAction)
            Assert.Equal("https://c1-test-cdr-ffiec.com/FFIEC/services/CdrService.asmx?WSDL", settings.Url)
            Assert.Equal("https://c1-test-cdr-ffiec.com/FFIEC/services/CdrService.asmx?WSDL", settings.UrlT)
            Assert.Equal("http://ffiec.gov/cdr/services/", settings.NS)
            Assert.Equal("http://ffiec.gov/cdr/services/SubmitInstanceData", settings.SubmitAction)
            Assert.Equal("http://ffiec.gov/cdr/services/SubmitTestInstanceData", settings.TestSubmitAction)
        End Sub
	End Class
End Namespace