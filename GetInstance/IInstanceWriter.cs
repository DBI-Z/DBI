namespace GetInstance
{
    internal interface IInstanceWriter
    {
        void Write(List<WriteFormat> records, string instanceFileName);
    }
}