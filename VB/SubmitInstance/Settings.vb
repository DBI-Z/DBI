Namespace SubmitInstance
	Public Class Settings
		Inherits ISettings

		Public Const SubmitRequestProd As String = "SubmitInstanceData"
		Public Const SubmitRequestTest As String = "SubmitTestInstanceData"
		Public Const SubmitResponseProd As String = "SubmitInstanceDataResponse"
		Public Const SubmitResponseTest As String = "SubmitTestInstanceDataResponse"
		Public Const SubmitResultProd As String = "SubmitInstanceDataResult"
		Public Const SubmitResultTest As String = "SubmitTestInstanceDataResult"

		Public ReadOnly Property GetAction As String
			Get
				Return GetValue("GetAction")
			End Get
		End Property

		Public ReadOnly Property Url As String
			Get
				Return GetValue("URL")
			End Get
		End Property

		Public ReadOnly Property UrlT As String
			Get
				Return GetValue("URLT")
			End Get
		End Property

		Public ReadOnly Property NS As String
			Get
				Return GetValue("NS")
			End Get
		End Property

		Public ReadOnly Property SubmitAction As String
			Get
				Return GetValue("SubmitAction")
			End Get
		End Property

		Public ReadOnly Property TestSubmitAction As String
			Get
				Return GetValue("TestSubmitAction")
			End Get
		End Property

		Private Function GetValue(ByVal key As String) As String
			Dim value As String = Nothing

			If String.IsNullOrWhiteSpace(key) Then
				Throw New ArgumentNullException(NameOf(key))
			ElseIf settings Is Nothing Then
				Throw New InvalidOperationException(NameOf(Load) & "must be called before reading a value")
			Else
				Dim loweredKey As String = key.ToLower()

				If settings.TryGetValue(loweredKey, value) Then
					Return value
				Else
					Return String.Empty
				End If
			End If
		End Function

		Private settings As Dictionary(Of String, String)

		Public Sub Load(ByVal stream As Stream)
			''' Cannot convert LocalDeclarationStatementSyntax, CONVERSION ERROR: Conversion for TupleType not implemented, please report this issue in '(stream)' at character 1315
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
			''' 			StreamReader sr = new(stream);

			''' 
			''' Cannot convert ExpressionStatementSyntax, CONVERSION ERROR: Conversion for TupleType not implemented, please report this issue in '()' at character 1342
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
			'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.MakeAssignmentStatement(AssignmentExpressionSyntax node)
			'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitAssignmentExpression(AssignmentExpressionSyntax node)
			'''    at Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
			'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
			'''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
			'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitAssignmentExpression(AssignmentExpressionSyntax node)
			'''    at Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
			'''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.ConvertSingleExpression(ExpressionSyntax node)
			'''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.VisitExpressionStatement(ExpressionStatementSyntax node)
			'''    at Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionStatementSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
			'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
			'''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
			'''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)
			''' 
			''' Input: 
			''' 			settings = new();

			''' 
			Dim line As String = Nothing

			While CSharpImpl.__Assign(line, TryCast(sr.ReadLine(), String)) IsNot Nothing
				Dim keyValue As String() = line.Split("="c, StringSplitOptions.TrimEntries)
				Dim loweredKey As String = keyValue(0).ToLower()
				settings(loweredKey) = keyValue(1)
			End While
		End Sub

		Private Class CSharpImpl
			<Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
			Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
				target = value
				Return value
			End Function
		End Class
	End Class
End Namespace
