Namespace SubmitInstance
	Friend Class InstanceReader
		Inherits IInstanceReader

		Public Function Read(ByVal xmlFileName As String) As String
			If File.Exists(xmlFileName) Then
				''' Cannot convert UsingStatementSyntax, CONVERSION ERROR: Conversion for TupleType not implemented, please report this issue in '(xmlFileName)' at character 188
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
				'''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.VisitUsingStatement(UsingStatementSyntax node)
				'''    at Microsoft.CodeAnalysis.CSharp.Syntax.UsingStatementSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
				'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
				'''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
				'''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)
				''' 
				''' Input: 
				''' 				using (StreamReader r = new(xmlFileName))
				Return r.ReadToEnd();

''' 
            Else
				Console.WriteLine($"File {xmlFileName} cannot be found.")
				Return String.Empty
			End If
		End Function
	End Class
End Namespace
