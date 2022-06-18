namespace ConsoleApp1
{
    internal class InstanceGetter
    {
        public InstanceGetter(IInstanceDownloader downloader, IInstanceWriter writer)
        {

        }

        public void DoSth()
        {
            const int numberOfPeriods = 6;
            const string contextRef = "CI_2631314_2005-03-31"; //TODO:input?
            const string period = "2022"; //TODO: input?
            string appFolder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            string instanceFileName = Path.Combine(appFolder, period + "-Instance.txt");

            string instanceXmlText = await GetInstanceData(param);
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
                doc.LoadXml(xmlI);
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

                    string yn = string.Empty;
                    Func<bool> isaYes = () => string.Compare("Y", yn?.Trim(), ignoreCase: true) == 0;
                    Func<bool> isaNo = () => string.Compare("N", yn?.Trim(), ignoreCase: true) == 0;

                    while (!isaYes())
                    {
                        yn = Console.ReadLine();
                        if (isaNo())
                        {
                            Console.WriteLine("Update History was not completed.");
                            Console.WriteLine("Live CDR Update Error");
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Update History was not completed.");
                }
            }
        }
    }
