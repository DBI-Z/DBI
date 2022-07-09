Imports System.Xml
Imports System.Xml.Serialization

<XmlRoot("GetInstanceData", [Namespace]:="http://ffiec.gov/cdr/services/")>
Public Class GetInstanceRequest
	Public Const DateFormat As String = "yyyy-MM-dd"
	<XmlElement("userName")>
	Public Property Username As String
	<XmlElement("password")>
	Public Property Password As String
	<XmlElement("dataSeriesName")>
	Public Property DataSeriesName As String
	<XmlElement("reportingPeriodEndDate")>
	Public Property ReportingPeriodEndDate As DateTime
	<XmlElement("id_rssd")>
	Public Property IdRssd As String
	<XmlElement("numberOfPriorPeriods")>
	Public Property NumberOfPriorPeriods As Integer
End Class
