using SimpleSOAPClient.Exceptions;
using SimpleSOAPClient.Helpers;
using SimpleSOAPClient.Models;
using System.Text;

namespace ConsoleApp1
{
    internal class InstanceDownloader : IInstanceDownloader
    {
        public async Task<string> GetInstanceData(GetInstanceRequest param)
        {
            var requestEnvelope =
                        SoapEnvelope.Prepare().Body(param);
            SoapEnvelope responseEnvelope = null;
            using (var client = new SoapHelper().GetInstance())
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
    }
}
