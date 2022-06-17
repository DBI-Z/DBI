using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    internal class Submitter
    {
        //using a login info, submit the contents of filename denoted at InstanceDataFileText.Text
        //either test or production server can be targeted
        public void DoSth(int numberOfPeriods)
        {
            Console.WriteLine("Submitting Call Report to the CDR");
            Console.WriteLine("1. Logging on to CDR");

            //submit call report by calling SubmitInstanceData()
            //Display Reader.RpcResult.Text
            //call GetRowCol to read certain cell contents (formula)? And Display them on the screen
            //Set the report date cell value
            //MsgBox Submitted.

            var DoSth2Result = DoSth2();
            var success = string.IsNullOrWhiteSpace(DoSth2Result.errmsg);
            if (success)
            {
                Console.WriteLine("Completed");
                //get the XML
                //load the XML
                //check whether <ErrorCode> is 0
            }
            else
            {
                Console.WriteLine("Error contacting CDR: Process aborted");
                Console.Write("Live Submit Call Report Error A: ");
                //Console.WriteLine(Reader.FaultString.Text);
            }
        }

        (string rpcresulttext, string errmsg) DoSth2()
        {
            return ("rpcr", "errmsg");
        }
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