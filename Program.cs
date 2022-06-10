using Microsoft.Extensions.Logging;
using SimpleSOAPClient;
using SimpleSOAPClient.Exceptions;
using SimpleSOAPClient.Handlers;
using SimpleSOAPClient.Helpers;
using SimpleSOAPClient.Models;
using SimpleSOAPClient.Models.Headers;
using SimpleSOAPClient.Models.Headers.Oasis.Security;
using System.Xml;
using System.Xml.Serialization;

//TODO: input NumberOfPeriods

Console.WriteLine("Updating history with prior quarter data from the CDR...");
Console.WriteLine("1. Logging on to CDR: ");

var res = await GetInstanceData();
if (res.Success)
{
    Console.WriteLine("3. Processing CDR Data: ");

}
else
{
    Console.WriteLine(res.Message); //errmsgFailed to deserialize the XML string to a SOAP Envelope'

}

//TODO: endit section

SoapClient PrepareClient()
{
    var a = SoapClient.Prepare()
          .WithHandler(new DelegatingSoapHandler
          {
              OnSoapEnvelopeRequestAsyncAction = async (c, d, cancellationToken) =>
              {
                  //          d.Envelope.WithHeaders(
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
             SoapEnvelope.Prepare().Body(new GetInstanceData());

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
            var response = responseEnvelope.Body<DummyResponse>();
            if (response.Res is string)
            {
                string a = response.Res.Replace("&gt;", ">").Replace("&lt;", "<");
                CdrServiceGetInstanceData dr = new XmlSerializer(typeof(CdrServiceGetInstanceData)).Deserialize(new StringReader(a)) as CdrServiceGetInstanceData;
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

[XmlRoot("DummyRequestRoot", Namespace = "http://ffiec.gov/cdr/services/")]
public class GetInstanceData
{
    [XmlElement("userName")]
    public bool Username { get; set; }
    [XmlElement("password")]
    public string Password { get; set; }
    [XmlElement("dataSeriesName")]
    public string DSN { get; set; }
    [XmlElement("reportingPeriodEndDate")]
    public string RPED { get; set; }
    [XmlElement("id_rssd")]
    public string RSSD { get; set; }
    [XmlElement("numberOfPriorPeriods")]
    public string nop { get; set; }
}

[XmlRoot("GetInstanceDataResponse", Namespace = "http://ffiec.gov/cdr/services/")]
public class DummyResponse
{
    [XmlElement("Inputs")]
    public Inputs Inputs { get; set; }

    [XmlElement("Outputs")]
    public Outputs? Outputs { get; set; }

    [XmlElement("GetInstanceDataResult")]
    public string Res { get; set; }
}


[XmlRoot("CdrServiceGetInstanceData", Namespace = "http://Cdr.Business.Workflow.Schemas.CdrServiceGetInstanceData")]
public class CdrServiceGetInstanceData
{
    [XmlElement("Inputs", Namespace = "")]
    public Inputs Inputs { get; set; }

    [XmlElement("Outputs", Namespace = "")]
    public Outputs? Outputs { get; set; }

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