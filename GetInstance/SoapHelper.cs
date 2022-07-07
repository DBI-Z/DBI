using SimpleSOAPClient;
using SimpleSOAPClient.Handlers;
using SimpleSOAPClient.Helpers;

namespace GetInstance
{
    internal class SoapHelper
    {
        SoapClient soapClient = null;
        public SoapClient GetInstance() => soapClient == null ? soapClient = Prepare() : soapClient;

        SoapClient Prepare()
        {

            var a = SoapClient.Prepare()
                        .WithHandler(new DelegatingSoapHandler
                        {
                            OnSoapEnvelopeRequestAsyncAction = async (c, d, cancellationToken) =>
                            {
                                //displayer.WriteLine(nameof(DelegatingSoapHandler.OnSoapEnvelopeRequestAsyncAction));
                                // d.Envelope.WithHeaders(
                                //KnownHeader.Oasis.Security.UsernameTokenAndPasswordText(
                                //    "some-user", "some-password"));
                            },
                            OnHttpRequestAsyncAction = async (soapClient, d, cancellationToken) =>
                            {
                                //displayer.WriteLine(nameof(DelegatingSoapHandler.OnHttpRequestAsyncAction));
                                // Logger.LogTrace(   "SOAP Outbound Request -> {0} {1}({2})\n{3}",  d.Request.Method, d.Url, d.Action, await d.Request.Content.ReadAsStringAsync());
                            },
                            OnHttpResponseAsyncAction = async (soapClient, d, cancellationToken) =>
                            {
                                //displayer.WriteLine(nameof(DelegatingSoapHandler.OnHttpResponseAsyncAction));
                                //   Logger.LogTrace(  "SOAP Outbound Response -> {0}({1}) {2} {3}\n{4}", d.Url, d.Action, (int)d.Response.StatusCode, d.Response.StatusCode, await d.Response.Content.ReadAsStringAsync());
                            },
                            OnSoapEnvelopeResponseAsyncAction = async (soapClient, d, cancellationToken) =>
                            {
                                //displayer.WriteLine(nameof(DelegatingSoapHandler.OnSoapEnvelopeResponseAsyncAction));
                            }
                        });
            return a;
        }
    }
}