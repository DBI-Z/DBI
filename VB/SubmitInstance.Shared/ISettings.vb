Imports System.IO

Public Interface ISettings
	Sub Load(ByVal stream As Stream)
	ReadOnly Property GetAction As String
	ReadOnly Property Url As String
	ReadOnly Property UrlT As String
	ReadOnly Property NS As String
	ReadOnly Property SubmitAction As String
	ReadOnly Property TestSubmitAction As String
End Interface