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

//TODO: input NumberOfPeriods

Console.WriteLine("Updating history with prior quarter data from the CDR...");
Console.WriteLine("1. Logging on to CDR: ");

var res = await GetInstanceData();
string period = "2022";
string contextRef = "CI_2631314_2005-03-31";
string appFolder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
string instanceFile = Path.Combine(appFolder, period + "-Instance.txt");

//if (res.Success)
{
    Console.WriteLine("3. Processing CDR Data: ");

    using (StreamWriter sr = new StreamWriter(instanceFile))
    //while (true)
    {
        //TODO
    }
    Console.WriteLine("Completed");
    Console.WriteLine("CDR Live Update: Successful");
    Console.WriteLine("Prior quarter history data has been downloaded successfully.");
}
//else
{
    //Console.WriteLine(res.Message); //errmsgFailed to deserialize the XML string to a SOAP Envelope'
}

//TODO:endit section
//if error downloading the file check if already exists and console.writeline already exists or Update History was not completed

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
                  var header =
        d.Envelope.Header<UsernameTokenAndPasswordTextSoapHeader>(
            "{" + Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssWssecuritySecext10 +
            "}Security");
              }
          });
    return a;
}

async Task<XBRLDownloadResult> GetInstanceData()
{
    //throw new NotImplementedException();

    using (var client = PrepareClient())
    {

        var requestEnvelope =
             SoapEnvelope.Prepare().Body(TestInput.TestRequest);

        CancellationToken ct = new();

        SoapEnvelope responseEnvelope;
        try
        {
            responseEnvelope =
                await client.SendAsync(
                    "https://cdr.ffiec.gov/cdr/Services/CdrService.asmx",
                    "http://ffiec.gov/cdr/services/GetInstanceData",
                    requestEnvelope, ct);
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

        try
        {
            var response = responseEnvelope.Body<GetInstanceDataResponse>();
            if (response.GetInstanceDataResult is string)
            {
                string instanceXml = response.GetInstanceDataResult.Replace("&gt;", ">").Replace("&lt;", "<");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(TestInputGetInstanceResponse.Test3);

                XPathNavigator n = doc.CreateNavigator();

                XmlNamespaceManager m = new XmlNamespaceManager(n.NameTable);
                m.AddNamespace("cc", "http://www.ffiec.gov/xbrl/call/concepts");

                if (n.Evaluate("//cc:*", m) is XPathNodeIterator ccElements)
                    while (ccElements.MoveNext())
                    {
                        var mtContextRef = ccElements.Current.GetAttribute("contextRef", string.Empty);

                        if (ccElements.Current.MoveToChild(XPathNodeType.Text))
                        {
                            if (bool.TryParse(ccElements.Current.Value, out bool mtBool))
                            {
                                ccElements.Current.MoveToParent();
                                Console.WriteLine(mtContextRef + " -> " + mtBool.ToString());
                            }
                            else
                            {
                                ccElements.Current.MoveToParent();
                                var mtText = ccElements.Current.Value;
                                var mtMdrm = ccElements.Current.Name;
                                var mtUnitRef = ccElements.Current.GetAttribute("unitRef", string.Empty);
                                var mtDecimals = ccElements.Current.GetAttribute("decimals", string.Empty);
                                Console.Write(mtContextRef + " -> " + mtText);
                                Console.Write(" mtMdrm=" + mtMdrm);
                                Console.Write(" mtUnitRef=" + mtUnitRef);
                                Console.Write(" mtUnitRef=" + mtUnitRef);
                                Console.WriteLine(" mtDecimals=" + mtDecimals);  
                            }
                        }
                        else
                        {
                            Console.WriteLine("no text child node?");
                        }
                    }
                //CdrServiceGetInstanceData dr = new  (typeof(CdrServiceGetInstanceData)).Deserialize(new StringReader(a)) as CdrServiceGetInstanceData;
                Console.WriteLine("dummy");
            }
            if (response.Outputs?.ErrorCode is int)
            {
                if (response.Outputs.ErrorMessage is string)
                {
                    //TODO: 
                    //do the main XML parsing and write to instance data
                }
                else
                {

                    string errmsg = "Error contacting CDR: Process aborted";
                }
            }
            else
            {
                string errmsg = "Live CDR Update Error B: ";
            }
        }
        catch (FaultException e)
        {
            //   Logger.LogError(e, $"The server returned a fault [Code={e.Code}, String={e.String}, Actor={e.Actor}]");
            throw;
        }
    }

    string[] ErrMsg = {
    "Error contacting CDR: Process aborted",
    "Live CDR Update Error A: " //TODO Reader.FaultString.Text
       };

    return new XBRLDownloadResult
    {
        MainText = "dummyMainText",
        Message = "dummyMessage",
        Success = true,
    };
}

class XBRLDownloadResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string MainText { get; set; } //TODO: Reader.RpcResult.Text
}

enum Code
{
    Error
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

//static class Logger
//{
//    static void LogError(Exception e ,string message)
//    {
//        throw new NotImplementedException();
//    }
//}