namespace GetInstance
{
    public interface IInstanceWriter
    {
        void Write(List<WriteFormat> records, string instanceFileName);
    }
}