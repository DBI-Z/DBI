namespace GetInstance.Test
{
	internal class ErrorResponse
	{
  public const string SoapBody = @"<GetInstanceDataResponse xmlns=""http://ffiec.gov/cdr/services/"">
            <GetInstanceDataResult>&lt;?xml version=""1.0""?&gt;
&lt;CdrServiceGetInstanceData xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""http://Cdr.Business.Workflow.Schemas.CdrServiceGetInstanceData""&gt;
  &lt;Inputs xmlns=""""&gt;
    &lt;ID_RSSD&gt;141958&lt;/ID_RSSD&gt;
    &lt;ReportingPeriodEndDate&gt;2022-03-31T17:19:24.614Z&lt;/ReportingPeriodEndDate&gt;
    &lt;NumberOfPriorPeriods&gt;6&lt;/NumberOfPriorPeriods&gt;
  &lt;/Inputs&gt;
  &lt;Outputs xmlns=""""&gt;
    &lt;ErrorCode&gt;-1&lt;/ErrorCode&gt;
    &lt;ErrorMessage&gt;Access denied&lt;/ErrorMessage&gt;
  &lt;/Outputs&gt;
&lt;/CdrServiceGetInstanceData&gt;</GetInstanceDataResult>
        </GetInstanceDataResponse>";

 }
}
