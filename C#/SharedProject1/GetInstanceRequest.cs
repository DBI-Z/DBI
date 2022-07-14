using System.Xml;
using System.Xml.Serialization;

[XmlRoot("GetInstanceData", Namespace = "http://ffiec.gov/cdr/services/")]
public class GetInstanceRequest
{
	public const string DateFormat = "yyyy-MM-dd";
	[XmlElement("userName")]
	public string Username { get; set; }
	[XmlElement("password")]
	public string Password { get; set; }
	[XmlElement("dataSeriesName")]
	public string DataSeriesName { get; set; }
	[XmlElement("reportingPeriodEndDate")]
	public DateTime ReportingPeriodEndDate { get; set; }
	[XmlElement("id_rssd")]
	public string IdRssd { get; set; }
	[XmlElement("numberOfPriorPeriods")]
	public int NumberOfPriorPeriods { get; set; }
}