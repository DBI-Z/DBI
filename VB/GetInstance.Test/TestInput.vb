Namespace GetInstance.Test
	Friend Class TestInput
		Public Shared ReadOnly Property TestRequest As GetInstanceRequest
			Get
				Return New GetInstanceRequest With {
								.Username = "DBIFINAN2",
								.Password = "Testingnewcode7~",
								.DataSeriesName = "call",
								.ReportingPeriodEndDate = New DateTime(2022, 3, 31),
								.IdRssd = "141958",
								.NumberOfPriorPeriods = 6
				}
			End Get
		End Property
	End Class
End Namespace