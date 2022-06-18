using System.Xml.XPath;
using SimpleSOAPClient;
using SimpleSOAPClient.Exceptions;
using SimpleSOAPClient.Handlers;
using SimpleSOAPClient.Helpers;
using SimpleSOAPClient.Models;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using ConsoleApp1;
using System.Text;
using System.Globalization;

const string dateFormat = "yyyy-MM-dd";

GetInstanceRequest param;
if (args.Length == 0)
{
    param = TestInput.TestRequest;
    PrintArgs(param);
}
else
{
    const int expectedArgCount = 6;
    if (args.Length != expectedArgCount)
    {
        param = null;
        Console.WriteLine("There should be exactly " + expectedArgCount + " arguments. Example:");
        PrintArgs(TestInput.TestRequest);
        return -1;
    }
    else
    {
        var cmdLineInput = new GetInstanceRequest
        {
            Username = args[0],
            Password = args[1],
            DataSeriesName = args[2],
            IdRssd = args[3]
        };
        if (int.TryParse(args[4], out int periods))cmdLineInput.NumberOfPriorPeriods = periods;
        else 
        {
            Console.WriteLine("NumberOfPriorPeriods should be a number. Example:");
            PrintArgs(TestInput.TestRequest);
            return -1;
        }
        if (DateTime.TryParseExact(args[5], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime endDate)) cmdLineInput.ReportingPeriodEndDate = endDate;
        else
        {
            Console.WriteLine($"ReportingPeriodEndDate should have date format {dateFormat}. Example:");
            PrintArgs(TestInput.TestRequest);
            return -1;
        }
        param = cmdLineInput;
    }
}

void PrintArgs(GetInstanceRequest args)
{
    string exeName = Process.GetCurrentProcess().MainModule.ModuleName;
    Console.Write(exeName + " ");
    Console.Write(args.Username + " ");
    Console.Write(args.Password + " ");
    Console.Write(args.DataSeriesName + " ");
    Console.Write(args.IdRssd + " ");
    Console.Write(args.NumberOfPriorPeriods + " ");
    Console.Write(args.ReportingPeriodEndDate.ToString(dateFormat) + " ");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"{exeName} Username Password DataSeriesName IdRSSD NumberOfPriorPeriods ReportingPeriodEndDate");
}

Console.WriteLine(string.Empty);
Console.WriteLine("Updating history with prior quarter data from the CDR...");
Console.WriteLine("1. Logging on to CDR: ");




async Task<SoapEnvelope> GetResponse(SoapClient client, SoapEnvelope requestEnv)
{
    try
    {
        var responseEnv = await client.SendAsync(
    "https://cdr.ffiec.gov/cdr/Services/CdrService.asmx",
    "http://ffiec.gov/cdr/services/GetInstanceData", requestEnv, new CancellationToken());
        return responseEnv;
    }
    catch (SoapEnvelopeSerializationException e)
    {
        //  Logger.LogError(e, $"Failed to serialize the SOAP Envelope [Envelope={e.Envelope}]");
        throw;
    }
    catch (SoapEnvelopeDeserializationException e)
    {
        //  Logger.LogError(e,   $"Failed to deserialize the response into a SOAP Envelope [XmlValue={e.XmlValue}]");
        throw;
    }
    catch (Exception ex)
    {
        Console.WriteLine("Unable to connect to the Internet. Some firewalls require altering permissions to allow EasyCall Report to communicate with the Central Data Repository (CDR).  Your information technology department should be made aware that this communication uses HTTPS via port 443.");
        Console.WriteLine("Also, the FFIEC CDR system now only supports TLS 1.2;  please insure your operating system supports said.");
    }
    return null;
}



List<WriteFormat> DeserializeRecords(XPathNodeIterator ccElements)
{
    List<WriteFormat> wf = new();

    while (ccElements.MoveNext())
    {
        var mtContextRef = ccElements.Current.GetAttribute("contextRef", string.Empty);

        if (ccElements.Current.MoveToChild(XPathNodeType.Text))
        {
            bool isConceptDataRecord = bool.TryParse(ccElements.Current.Value, out bool mtBool);
            if (isConceptDataRecord)
            {
                ccElements.Current.MoveToParent();
                Console.WriteLine(mtContextRef + " -> " + mtBool.ToString());

                wf.Add(new WriteFormat
                {
                    MtData = mtBool.ToString(),
                    MtContextRef = mtContextRef
                }); ;
            }
            else
            {
                ccElements.Current.MoveToParent();
                string? mtText = ccElements.Current.Value;
                string? mtMdrm = ccElements.Current.LocalName;
                string? mtUnitRef = ccElements.Current.GetAttribute("unitRef", string.Empty);
                string? mtDecimals = ccElements.Current.GetAttribute("decimals", string.Empty);

                wf.Add(new WriteFormat
                {
                    MtDecimals = mtDecimals,
                    MtData = mtText,
                    MtMdrm = mtMdrm,
                    MtUnitRef = mtUnitRef,
                    MtContextRef = mtContextRef
                });
            }
        }
        else
        {
            Console.WriteLine("no text child node?");
        }
    }

    return wf;
} 

return 0;

[XmlRoot("GetInstanceData", Namespace = "http://ffiec.gov/cdr/services/")]
public class GetInstanceRequest
{
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

[XmlRoot("GetInstanceDataResponse", Namespace = "http://ffiec.gov/cdr/services/")]
public class GetInstanceDataResponse
{
    [XmlElement("Inputs")]
    public Inputs Inputs { get; set; }

    [XmlElement("Outputs")]
    public Outputs? Outputs { get; set; }

    [XmlElement("GetInstanceDataResult")]
    public string GetInstanceDataResult { get; set; }
}

public class Inputs
{
    [XmlElement("RSSD")]
    public int RSSD;
    [XmlElement("ReportingPeriodEndDate")]
    public DateTime ReportingPeriodEndDate { get; set; }
    [XmlElement("NumberOfPriorPeriods")]
    public int NumberOfPriorPeriods { get; set; }
}

public class Outputs
{
    [XmlElement("ErrorCode")]
    public int ErrorCode { get; set; }
    [XmlElement("ErrorMessage")]
    public string ErrorMessage { get; set; }
}


//conceptRecord write format  Write #IFN, MT_MDRM, MT_ContextRef, "", "0", MT_Data
//otherRecord writeFormat Write #IFN, MT_MDRM, MT_ContextRef, MT_UnitRef, MT_Decimals, MT_Data

class WriteFormat
{
    public string MtMdrm { get; set; }
    public string MtUnitRef { get; set; }
    public string MtDecimals { get; set; }
    public string MtContextRef { get; set; }
    public string MtData { get; set; }

    //??
    //currentPeriod = InStr(contextRef, Period)
    //mtData = contextRef.IndexOf(Period) > -1 ? "Y" : "true;
    //mtData = contextRef.IndexOf(Period) > -1 ? "N" : "false";
    //??
    //									  currentPeriod = InStr(contextRef, Period)
    //GetRowCol 0, "PERIOD", 1, SheetName, SheetNum, CRow, CCol, CValue, CText
    //Period = Trim(CText)
}

