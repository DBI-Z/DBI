using SimpleSOAPClient.Helpers;
using SimpleSOAPClient.Models;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace ConsoleApp1
{
    internal class Submitter
    {
        //using a login info, submit the contents of filename denoted at InstanceDataFileText.Text
        //either test or production server can be targeted
        public async void DoSth(int numberOfPeriods, string username, string password)
        {
            Console.WriteLine("Submitting Call Report to the CDR");
            Console.WriteLine("1. Logging on to CDR");

            //submit call report by calling SubmitInstanceData()
            //Display Reader.RpcResult.Text
            //call GetRowCol to read certain cell contents (formula)? And Display them on the screen
            //Set the report date cell value
            //MsgBox Submitted.

            var serviceUrl = "https://cdr.ffiec.gov/cdr/Services/CdrService.asmx"; //read from .json, .ini etc
            var instanceXml = ""; //read from file (convert to XML up-front if required)
            var action = "http://ffiec.gov/cdr/services/SubmitInstanceData"; //CDR.ini
            var requestParam = new SubmitInstanceDataRequest
            {
                InstanceData = instanceXml,
                Password = password,
                UserName = username
            };

            Console.WriteLine("Logged on");
            Console.WriteLine("2. Submitting Call Report: ");

            XElement DoSth2Result = null;

            try
            {
                DoSth2Result = await Submit(serviceUrl, requestParam, action);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string errmsg =
                  "Unable to connect.  Some firewalls require altering permissions to allow EasyCall Report to communicate with the Central Data Repository.  Your information technology department should be made aware that this communication uses HTTPS via port 443." +
                  "Also, the FFIEC CDR system now only supports TLS 1.2;  please insure your operating system supports said.";
                Console.WriteLine(PrepareMsg(errmsg));
                return;
            }

            Console.WriteLine(DoSth2Result); //serialized xml? object name?
            XPathNavigator nav = DoSth2Result.CreateNavigator();
            XmlNamespaceManager m = new(nav.NameTable);

            m.AddNamespace(string.Empty, "http://Cdr.Business.Workflow.Schemas.CdrServiceSubmitInstanceData");
            XPathNodeIterator it = nav.Evaluate("/CdrServiceGetInstanceData/Outputs/ErrorCode") as XPathNodeIterator;
            if (it == null || !it.MoveNext())
            {
                Console.WriteLine("Error contacting CDR: Process aborted");
                Console.Write("Live Submit Call Report Error B2: ");
                Console.WriteLine(Reader.FaultString.Text);
                SuggestWebsiteOpen();
            }
            else
            {
                int errorCode;
                try
                {
                    errorCode = it.Current.ValueAsInt;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error contacting CDR: Process aborted");
                    Console.Write("Live Submit Call Report Error B2: ");
                    Console.WriteLine(ex.Message);
                    return;
                }
                if (errorCode == 0)
                {
                    Console.WriteLine("Live Submit Call Report");
                    Console.WriteLine("Your Call Report has been submitted to the CDR.");
                    Console.WriteLine("This DOES NOT mean it has been accepted by the CDR.");
                    Console.WriteLine("You must wait for a CDR email that will indicate whether your Call Report has been accepted or rejected.");
                    Console.WriteLine("The e-mail message will be sent to the following:");
                    Console.WriteLine("    Authorized Officer Signing the Reports: TEXTC490 <TEXTC492>");
                    Console.WriteLine("    Other Person to Whom Questions about the");
                    Console.WriteLine("       Reports Should be Directed: TEXTC495 <TEXT4086>"); 
                }
                else
                {
                    it = nav.Evaluate("/CdrServiceGetInstanceData/Outputs/ErrorMessage") as XPathNodeIterator;
                    it.MoveNext();
                    string errMsg = it.Current.Value;
                    Console.WriteLine(PrepareMsg(errMsg));
                }
            } 
        }

        private void SuggestWebsiteOpen()
        {
            Console.WriteLine("Would you like to exit EasyCALL and try submitting your Call Report directly to the CDR website?");
            Console.WriteLine("Live CDR Submit Error");
            Console.WriteLine("Would you like to exit EasyCALL and try submitting your Call Report directly to the CDR website?");

            string yn = string.Empty;
            Func<bool> isaYes = () => string.Compare("Y", yn?.Trim(), ignoreCase: true) == 0;
            Func<bool> isaNo = () => string.Compare("N", yn?.Trim(), ignoreCase: true) == 0;

            for (yn = Console.ReadLine(); !isaNo(); yn = Console.ReadLine())
            {
                if (isaYes())
                {
                    //navigate to "https://cdr.ffiec.gov/CDR/Login.aspx"
                    break;
                }
            }
        }

        async Task<XElement> Submit(string server, SubmitInstanceDataRequest param, string action)
        {
            var requestEnvelope = SoapEnvelope.Prepare().Body(param);
            var client = new SoapHelper().GetInstance();
            var responseEnvelope = await client.SendAsync(server, action, requestEnvelope);
            return responseEnvelope.Body.Value;
        }

        string PrepareMsg(string subText)
        {
            const int defaultErrorCode = 6;
            (string phrase, int code)[] errorPhrases = new[]
            {
                ("not authorized",1 ),
                ("not match",2 ),
                ("Access denied",3 ),
                ("valid XML",4 ),
                ("ID_RSSD",5 )
            };

            StringBuilder sb = new();

            int errorCode = defaultErrorCode;
            for (int i = 0; i < errorPhrases.Length; i++)
                if (subText.IndexOf(errorPhrases[i].phrase) > 0)
                    errorCode = i;

            sb.AppendLine("A problem was encountered while attempting to submit your Call Report to the CDR.");
            switch (errorCode)
            {
                case 1:
                    sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.");
                    break;
                case 4:
                    sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.");
                    break;
                case 5:
                case 6:
                    sb.AppendJoin("Error", errorCode, subText);
                    break;
                case 2:
                    sb.AppendLine("Invalid User Name or Password, or User is locked.");
                    sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.");
                    break;
                    break;
                case 3:
                    sb.AppendLine("Invalid User Name or Password, User is locked, or mandatory training must be completed.");
                    break;
                    sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.");
                    break;
            }

            sb.AppendLine("Contact the FFIEC CDR Help Desk at toll-free (888)237-3111 for assistance.");
            return sb.ToString();
        }
    }

    [XmlRoot("SubmitInstanceData", Namespace = "http://ffiec.gov/cdr/services/")]
    public class SubmitInstanceDataRequest
    {
        [XmlElement("userName")]
        public string UserName { get; set; }
        [XmlElement("Password")]
        public string Password { get; set; }
        [XmlElement("instanceData")]
        public string InstanceData { get; set; }

    }

    [XmlRoot("SetInstanceDataResponse", Namespace = "http://ffiec.gov/cdr/services/")]
    public class GetInstanceDataResponse
    {
        [XmlElement("Inputs")]
        public Inputs Inputs { get; set; }

        [XmlElement("Outputs")]
        public Outputs? Outputs { get; set; }

        [XmlElement("GetInstanceDataResult")]
        public string GetInstanceDataResult { get; set; }
    }
}