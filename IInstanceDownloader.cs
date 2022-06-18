namespace GetInstance
{
    internal interface IInstanceDownloader
    {
        Task<string> Download(GetInstanceRequest param);
    }
}
