Namespace SubmitInstance
	Friend Class TestInput
		Public Shared ReadOnly Property TestParam As SubmitParam
			Get
				Return New SubmitParam With {
								.Username = "DBIFINAN2",
								.Password = "TestingNew12",
								.Filename = "ffiec_141958_2022-03-31.xml",
								.Prod = True
				}
			End Get
		End Property
	End Class
End Namespace
