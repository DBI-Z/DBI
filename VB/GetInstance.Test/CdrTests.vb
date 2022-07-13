Imports System.IO
Imports Xunit
Imports Moq

Namespace GetInstance.Test
	Public Class CdrTests
		<Fact>
		Public Sub Extract_ErrorResponse_AccessDenied()
			Dim displayerStub As Mock(Of IDisplayer) = New Mock(Of IDisplayer)()
			Dim cdr As Cdr = New Cdr(displayerStub.Object)
			Dim errorResponseDocument As XDocument = XDocument.Load(New StringReader(ErrorResponse.SoapBody))
			Dim extracted = cdr.ExtractGetResponse(errorResponseDocument)
			Assert.[True](extracted.Code = -1)
			Assert.[True](extracted.Message = "Access denied")
			Assert.Equal(0, extracted.InstanceDocuments.Elements().Count())
		End Sub

		<Fact>
		Public Sub Extract_SpecResponse_CodeIsZero()
			Dim displayerStub As Mock(Of IDisplayer) = New Mock(Of IDisplayer)
			Dim cdr As Cdr = New Cdr(displayerStub.Object)
			Dim specResponse As XDocument = XDocument.Load(New StringReader(TestInputGetInstanceResponse.SuccessfulSpecResponse))
			Dim extracted As GetInstanceResponse = Cdr.ExtractGetResponse(specResponse)
			Assert.Equal(0, extracted.Code)
			Assert.Equal("Success - 2 instance documents found.", extracted.Message)
			Assert.NotNull(extracted.InstanceDocuments)
			Assert.Equal(1, extracted.InstanceDocuments.Elements().Count())
		End Sub

		<Fact>
		Public Sub Extract_SuccessfulResponse_CodeIsZero()
			Dim displayerStub As Mock(Of IDisplayer) = New Mock(Of IDisplayer)
			Dim cdr As Cdr = New Cdr(displayerStub.Object)
			Dim errorResponse As XDocument = XDocument.Load(New StringReader(SuccessfulResponse1.SoapBody))
			Dim extracted As GetInstanceResponse = cdr.ExtractGetResponse(errorResponse)
            Dim ins As IEnumerable(Of XElement) = extracted?.InstanceDocuments?.Element("InstanceDocuments")?.Elements()
            Assert.Equal(0, extracted.Code)
            Assert.Equal("Success - 6 instance documents found.", extracted.Message)
            Assert.NotNull(ins)
            Assert.Equal(6, ins.Count())
        End Sub
    End Class
End Namespace