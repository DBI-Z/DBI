Imports System.Diagnostics
Imports System.Globalization

Namespace GetInstance
	Friend Class ArgumentProcessor
		Private args As String()
		Private displayer As IDisplayer

		Public Sub New(ByVal args As String(), ByVal displayer As IDisplayer)
			Me.args = args
			Me.displayer = displayer
		End Sub

		Public Function GetRequest() As GetInstanceRequest
			Const expectedArgCount As Integer = 6
			Dim request As GetInstanceRequest
			Dim periods As Integer = Nothing, endDate As DateTime = Nothing

			If args.Length = expectedArgCount Then
				request = New GetInstanceRequest With {
								.Username = args(0),
								.Password = args(1),
								.DataSeriesName = args(2),
								.IdRssd = args(3)
				}

				If Integer.TryParse(args(4), periods) Then
					request.NumberOfPriorPeriods = periods

					If DateTime.TryParseExact(args(5), GetInstanceRequest.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, endDate) Then
						request.ReportingPeriodEndDate = endDate
					Else
						displayer.WriteLine($"ReportingPeriodEndDate should have date format {GetInstanceRequest.DateFormat}. Example:")
						PrintArgs(TestRequest)
						request = Nothing
					End If
				Else
					displayer.WriteLine("NumberOfPriorPeriods should be a number. Example:")
					PrintArgs(TestRequest)
					request = Nothing
				End If
			ElseIf args.Length = 0 Then
				request = Nothing
			Else
				displayer.WriteLine("There should be exactly " & expectedArgCount & " arguments. Example:")
				PrintArgs(TestRequest)
				request = Nothing
			End If

			Return request
		End Function

		Public Sub PrintArgs(ByVal args As GetInstanceRequest)
			Dim exeName As String = Process.GetCurrentProcess().MainModule.ModuleName
			displayer.Write(exeName & " ")
			displayer.Write(args.Username & " ")
			displayer.Write(New String("*"c, args.Password.Length) & " ")
			displayer.Write(args.DataSeriesName & " ")
			displayer.Write(args.IdRssd & " ")
			displayer.Write(args.NumberOfPriorPeriods & " ")
			displayer.Write(args.ReportingPeriodEndDate.ToString(GetInstanceRequest.DateFormat) & " ")
			displayer.WriteLine(String.Empty)
			displayer.WriteLine($"{exeName} Username Password DataSeriesName IdRSSD NumberOfPriorPeriods ReportingPeriodEndDate")
		End Sub

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