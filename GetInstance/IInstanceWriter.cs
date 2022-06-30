namespace GetInstance
{
    public interface IInstanceWriter
    {
        void Write(IEnumerable<WriteFormat> records, Stream instanceFile);
    }
}