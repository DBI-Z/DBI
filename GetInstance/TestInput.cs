namespace GetInstance
{
    internal class TestInput
    {
        public static GetInstanceRequest TestRequest =>
            new GetInstanceRequest
            {
                Username = "DBIFINAN2",
                Password = "Testingnewcode7~",
                DataSeriesName = "call",
                ReportingPeriodEndDate = new DateTime(2022, 03, 31),
                IdRssd = "141958",
                NumberOfPriorPeriods = 6
            };
    }
}