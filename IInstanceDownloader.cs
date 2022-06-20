using System.Xml.Linq; 

namespace GetInstance
{
    internal interface IInstanceDownloader
    {
        Task<XDocument> Download(GetInstanceRequest param);
    }
}
