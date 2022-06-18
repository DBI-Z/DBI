namespace GetInstance
{
    internal class InstanceFileWriter : IInstanceWriter
    {
        public void Write(List<WriteFormat> records, string instanceFileName)
        {
            using (StreamWriter sw = new(instanceFileName, append: false))
                foreach (WriteFormat a in records)
                {
                    string csv = string.Join(',', a.MtMdrm, a.MtContextRef, a.MtUnitRef, a.MtDecimals, a.MtData);
                    Console.WriteLine(csv);
                    sw.WriteLine(csv);
                }
        }
    }
}
