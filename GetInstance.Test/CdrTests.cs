using Moq;
using System.Xml.Linq;

namespace GetInstance.Test
{
	public class CdrTests
	{
		[Fact]
		public void Extract_ErrorResponse_AccessDenied()
		{
			//Arrange
			Mock<IDisplayer> displayerStub = new Mock<IDisplayer>();

			Cdr cdr = new(displayerStub.Object);
			XDocument errorResponse = XDocument.Load(new StringReader(ErrorResponse.SoapBody));

			//Act
			var extracted = cdr.ExtractGetResponse(errorResponse);

			//Assert
			Assert.True(extracted.Code == -1);
			Assert.True(extracted.Message == "Access denied");
			Assert.Equal(0, extracted.InstanceDocuments.Elements().Count());
		}

		[Fact]
		public void Extract_SpecResponse_CodeIsZero()
		{
			//Arrange
			Mock<IDisplayer> displayerStub = new();
			Cdr cdr = new(displayerStub.Object);
			XDocument specResponse = XDocument.Load(new StringReader(TestInputGetInstanceResponse.SuccessfulSpecResponse));

			//Act
			GetInstanceResponse extracted = cdr.ExtractGetResponse(specResponse);

			//Assert
			Assert.Equal(0, extracted.Code);
			Assert.Equal("Success - 2 instance documents found.", extracted.Message);
			Assert.NotNull(extracted.InstanceDocuments);
			Assert.Equal(1, extracted.InstanceDocuments.Elements().Count());
		}

		[Fact]
		public void Extract_SuccessfulResponse_CodeIsZero()
		{
			//Arrange
			Mock<IDisplayer> displayerStub = new();
			Cdr cdr = new(displayerStub.Object);
			XDocument errorResponse = XDocument.Load(new StringReader(SuccessfulResponse1.SoapBody));

			//Act
			GetInstanceResponse extracted = cdr.ExtractGetResponse(errorResponse);
			IEnumerable<XElement> ins = extracted?.InstanceDocuments?.Element("InstanceDocuments")?.Elements();

			//Assert
			Assert.Equal(0, extracted.Code);
			Assert.Equal("Success - 6 instance documents found.", extracted.Message);
			Assert.NotNull(ins);
			Assert.Equal(6, ins.Count());
		}
	}
}
