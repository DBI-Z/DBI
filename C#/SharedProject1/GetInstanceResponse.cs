using System.Xml.Linq;

namespace GetInstance
{
	public class GetInstanceResponse
	{
		public int? Code { get; set; }
		public string? Message { get; set; }
		public XDocument? InstanceDocuments { get; set; }
	}
}
