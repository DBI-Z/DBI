namespace GetInstance.Test
{
	internal class SuccessfulEmptyResponse
	{
		public static readonly string SoapBody = 
   soapActionHader +
   cdrHeader.Replace("<", "&lt;").Replace(">", "&gt;") +
   xbrl.Replace("<", "&lt;").Replace(">", "&gt;") +
   cdrFooter.Replace("<", "&lt;").Replace(">", "&gt;") +
   soapActionFooter;

		const string soapActionHader = @"
     <GetInstanceDataResponse xmlns=""http://ffiec.gov/cdr/services/"">
     <GetInstanceDataResult>";

		const string cdrHeader = @"<?xml version=""1.0""?>
    <CdrServiceGetInstanceData xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
      xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
      xmlns=""http://Cdr.Business.Workflow.Schemas.CdrServiceGetInstanceData"">
      <Inputs xmlns="""">
        <ID_RSSD>968605</ID_RSSD>
        <DataSeriesName>call</DataSeriesName>
        <ReportingPeriodEndDate>2004-09-30T15:01:29.0000000-04:00</ReportingPeriodEndDate>
        <NumberOfPriorPeriods>1</NumberOfPriorPeriods>
      </Inputs>
      <Outputs xmlns="""">
        <ReturnCode>0</ReturnCode>
        <ReturnMessage>Success - 2 instance documents found.</ReturnMessage>
        <InstanceDocuments>
          <InstanceDocument>";

		const string xbrl = @"<xbrl xmlns=""http://www.xbrl.org/2003/instance"">test</xbrl>";

		const string cdrFooter = @"
     </InstanceDocument>
         </InstanceDocuments>
       </Outputs>
     </CdrServiceGetInstanceData>";

  const string soapActionFooter = @"</GetInstanceDataResult>
        </GetInstanceDataResponse>";
	}
}
