using System.Xml.Linq; 

namespace GetInstance
{
    public interface IInstanceDownloader
    {
        Task<XDocument> Download(GetInstanceRequest param);
    }
}