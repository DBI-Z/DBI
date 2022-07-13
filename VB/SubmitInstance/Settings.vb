Imports System.IO

Public Class Settings
		Implements ISettings

		Public Const SubmitRequestProd As String = "SubmitInstanceData"
		Public Const SubmitRequestTest As String = "SubmitTestInstanceData"
		Public Const SubmitResponseProd As String = "SubmitInstanceDataResponse"
		Public Const SubmitResponseTest As String = "SubmitTestInstanceDataResponse"
		Public Const SubmitResultProd As String = "SubmitInstanceDataResult"
		Public Const SubmitResultTest As String = "SubmitTestInstanceDataResult"

		Public ReadOnly Property GetAction As String Implements ISettings.GetAction
			Get
				Return GetValue("GetAction")
			End Get
		End Property

		Public ReadOnly Property Url As String Implements ISettings.Url
			Get
				Return GetValue("URL")
			End Get
		End Property

		Public ReadOnly Property UrlT As String Implements ISettings.UrlT
			Get
				Return GetValue("URLT")
			End Get
		End Property

		Public ReadOnly Property NS As String Implements ISettings.NS
			Get
				Return GetValue("NS")
			End Get
		End Property

		Public ReadOnly Property SubmitAction As String Implements ISettings.SubmitAction
			Get
				Return GetValue("SubmitAction")
			End Get
		End Property

		Public ReadOnly Property TestSubmitAction As String Implements ISettings.TestSubmitAction
			Get
				Return GetValue("TestSubmitAction")
			End Get
		End Property

		Function GetValue(ByVal key As String) As String
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

		Dim settings As Dictionary(Of String, String)

		Public Sub Load(ByVal stream As Stream) Implements ISettings.Load
			Dim sr As StreamReader = New StreamReader(stream)
			settings = New Dictionary(Of String, String)
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
