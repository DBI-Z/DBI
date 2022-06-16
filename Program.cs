using System.Xml.XPath;
using SimpleSOAPClient;
using SimpleSOAPClient.Exceptions;
using SimpleSOAPClient.Handlers;
using SimpleSOAPClient.Helpers;
using SimpleSOAPClient.Models;
using SimpleSOAPClient.Models.Headers.Oasis.Security;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using ConsoleApp1;
using System.Text;

//TODO: input NumberOfPeriods

Console.WriteLine("Updating history with prior quarter data from the CDR...");
Console.WriteLine("1. Logging on to CDR: ");

string contextRef = "CI_2631314_2005-03-31"; //TODO:input?
string period = "2022"; //TODO: input?
string appFolder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
string instanceFileName = Path.Combine(appFolder, period + "-Instance.txt");


string instanceXmlText = await GetInstanceData();
if (string.IsNullOrWhiteSpace(instanceXmlText))
{
    AskPreviousFile();
}
else
{
    Console.WriteLine("3. Processing CDR Data: ");
    XPathNodeIterator ccRecordsIterator = GetCCIterator(instanceXmlText);
    List<WriteFormat> wf = DeserializeRecords(ccRecordsIterator);
    WriteToFile(records: wf, instanceFileName);
    Console.WriteLine("Completed");
    Console.WriteLine("CDR Live Update: Successful");
    Console.WriteLine("Prior quarter history data has been downloaded successfully.");
}

XPathNodeIterator GetCCIterator(string xmlI)
{
    XmlDocument doc = new XmlDocument();
    doc.LoadXml(TestInputGetInstanceResponse.Test3);
    XPathNavigator n = doc.CreateNavigator();
    XmlNamespaceManager m = new XmlNamespaceManager(n.NameTable);
    m.AddNamespace("cc", "http://www.ffiec.gov/xbrl/call/concepts");
    return n.Evaluate("//cc:*", m) as XPathNodeIterator;
}

void AskPreviousFile()
{

    if (File.Exists(instanceFileName))
    {
        Console.WriteLine("Although the Live CDR Update was not successful, prior quarter data is available on your hard drive.");
        Console.WriteLine("It should be safe to use this data in verifying your Call Report.");
        Console.WriteLine("Do you want to continue Updating History with this prior quarter data? Y/N");
        string yn = Console.ReadLine();

        Func<bool> isaYes = () => string.Compare("Y", yn?.Trim(), ignoreCase: true)== 0;
        Func<bool> isaNo = () => string.Compare("N", yn?.Trim(), ignoreCase: true) == 0;

        while (!isaYes())
            if (isaNo())
            {
                Console.WriteLine("Update History was not completed.");
                Console.WriteLine("Live CDR Update Error");
            }
    }
    else
    {
        Console.WriteLine("Update History was not completed."); 
    }  
} 
SoapClient PrepareClient()
{

    var a = SoapClient.Prepare()
                .WithHandler(new DelegatingSoapHandler
                {
                    OnSoapEnvelopeRequestAsyncAction = async (c, d, cancellationToken) =>
                        {
                            // d.Envelope.WithHeaders(
                            //KnownHeader.Oasis.Security.UsernameTokenAndPasswordText(
                            //    "some-user", "some-password"));
                        },
                    OnHttpRequestAsyncAction = async (soapClient, d, cancellationToken) =>
                        {
                            // Logger.LogTrace(   "SOAP Outbound Request -> {0} {1}({2})\n{3}",  d.Request.Method, d.Url, d.Action, await d.Request.Content.ReadAsStringAsync());
                        },
                    OnHttpResponseAsyncAction = async (soapClient, d, cancellationToken) =>
                        {
                            //   Logger.LogTrace(  "SOAP Outbound Response -> {0}({1}) {2} {3}\n{4}", d.Url, d.Action, (int)d.Response.StatusCode, d.Response.StatusCode, await d.Response.Content.ReadAsStringAsync());
                        },
                    OnSoapEnvelopeResponseAsyncAction = async (soapClient, d, cancellationToken) =>
                        {

                        }
                });
    return a;
}

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

async Task<string> GetInstanceData()
{
    var requestEnvelope =
                SoapEnvelope.Prepare().Body(TestInput.TestRequest);
    SoapEnvelope responseEnvelope = null;
    using (var client = PrepareClient())
        responseEnvelope = await GetResponse(client, requestEnvelope);

    if (responseEnvelope == null) return null;
    else
        try
        {
            var response = responseEnvelope.Body<GetInstanceDataResponse>();
            Console.WriteLine(response.GetInstanceDataResult);

            if (response.Outputs?.ErrorCode is int)
            {
                bool successResponse = response.Outputs.ErrorMessage is string &&
                        !string.IsNullOrWhiteSpace(response.Outputs.ErrorMessage);
                if (successResponse)
                {
                    if (response.GetInstanceDataResult is string)
                    {
                        string instanceXml = response.GetInstanceDataResult.Replace("&gt;", ">").Replace("&lt;", "<");
                        return instanceXml;

                        //open a file for text output
                        //conceptRecord write format  Write #IFN, MT_MDRM, MT_ContextRef, "", "0", MT_Data
                        //otherRecord writeFormat     Write #IFN, MT_MDRM, MT_ContextRef, MT_UnitRef, MT_Decimals, MT_Data
                    }
                    else
                    {
                        Console.WriteLine("Error: " + nameof(response.GetInstanceDataResult) + " element not found in the response XML");
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine("Error contacting CDR: Process aborted");
                    Console.Write(PrepareMsg(response.Outputs.ErrorMessage));
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Live CDR Update Error B: ");
                return null;
            }
        }
        catch (FaultException e)
        {
            //   Logger.LogError(e, $"The server returned a fault [Code={e.Code}, String={e.String}, Actor={e.Actor}]");
            throw;
        }
}
void WriteToFile(List<WriteFormat> records, string fileNamek)
{
    using (StreamWriter sw = new(instanceFileName, append: false))
        foreach (WriteFormat a in records)
        {
            string csv = string.Join(',', a.MtMdrm, a.MtContextRef, a.MtUnitRef, a.MtDecimals, a.MtData);
            Console.WriteLine(csv);
            sw.WriteLine(csv);
        }
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

string PrepareMsg(string responseErrorMessage)
{
    const int defaultErrorCode = 6;
    (string phrase, int code)[] errorPhrases = new[]
    {
        ("not authorized",1 ),
        ("not match",2 ),
        ("Access denied",3 ),
        ("ID_RSSD",4 )
    };

    StringBuilder sb = new();

    int errorCode = defaultErrorCode;
    for (int i = 0; i < errorPhrases.Length; i++)
        if (responseErrorMessage.IndexOf(errorPhrases[i].phrase) > 0)
            errorCode = i;

    sb.AppendLine("A problem was encountered while attempting to contact the CDR to download prior quarter history data.");
    switch (errorCode)
    {
        case 1:
        case 4:
        case 6:
            sb.AppendJoin("Error", errorCode, responseErrorMessage);
            break;
        case 2:
        case 3:
            sb.AppendLine("Invalid User Name or Password, or User is locked.");
            break;
    }
    sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.");
    return sb.ToString();
}

[XmlRoot("GetInstanceData", Namespace = "http://ffiec.gov/cdr/services/")]
public class GetInstanceData
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
    public string MtMdrm {   get; set; }
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

