namespace GetInstance
{
    internal class TestInputGetInstanceResponse
    {
        public static string Test1 = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
            <xbrl xmlns = ""http://www.xbrl.org/2003/instance""
            xmlns:link=""http://www.xbrl.org/2003/linkbase""
            xmlns:xlink=""http://www.w3.org/1999/xlink""
            xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
            xmlns:xbrli=""http://www.xbrl.org/2003/instance""
            xmlns:xbrldi=""http://xbrl.org/2006/xbrldi""
            xmlns:ffieci=""http://www.ffiec.gov/cdr""
            xmlns:iso4217=""http://www.xbrl.org/2003/iso4217""
            xmlns:ffiec031-2007-06-30=""http://www.ffiec.gov/xbrl/call/report031/2007-06-30/v34""
            xmlns:cc=""http://www.ffiec.gov/xbrl/call/concepts""
            xsi:schemaLocation=""http://www.ffiec.gov/xbrl/call/report031/2007-06-30/v34/
            call-report031-2007-06-30-v34.xsd http://www.ffiec.gov/xbrl/call/concepts
            concepts.xsd"">
              <cc:test>abcdef</cc:test>
                <a>
                    <xbrl2></xbrl2>
                    <cc:xbrl2></cc:xbrl2>
                </a>
            </xbrl>";
         
        public static string Test2 = @"<GetInstanceDataResponse><Outputs><cc:RCON1480 contextRef=""CI_1402629_2003-03-31"" unitRef=""USD"" decimals=""0"">34984000</cc:RCON1480><cc:RCON4088 contextRef=""CI_2631314_2005-03-31"">true</cc:RCON4088></Outputs></GetInstanceDataResponse>";
        public static string TestBook = @"<?xml version=""1.0"" encoding=""utf-8"" ?>   <bookstore>      <book genre=""autobiography"" publicationdate=""1981-03-22"" ISBN=""1-861003-11-0"">          <title>The Autobiography of Benjamin Franklin</title>          <author>              <first-name>Benjamin</first-name>              <last-name>Franklin</last-name>          </author>          <price>8.99</price>      </book>      <book genre=""novel"" publicationdate=""1967-11-17"" ISBN=""0-201-63361-2"">          <title>The Confidence Man</title>          <author>              <first-name>Herman</first-name>              <last-name>Melville</last-name>          </author>          <price>11.99</price>      </book>      <book genre=""philosophy"" publicationdate=""1991-02-15"" ISBN=""1-861001-57-6"">          <title>The Gorgias</title>          <author>              <name>Plato</name>          </author>          <price>9.99</price>      </book>  </bookstore>  ";
        public static string Test3 = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
            <xbrl xmlns = ""http://www.xbrl.org/2003/instance""
            xmlns:link=""http://www.xbrl.org/2003/linkbase""
            xmlns:xlink=""http://www.w3.org/1999/xlink""
            xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
            xmlns:xbrli=""http://www.xbrl.org/2003/instance""
            xmlns:xbrldi=""http://xbrl.org/2006/xbrldi""
            xmlns:ffieci=""http://www.ffiec.gov/cdr""
            xmlns:iso4217=""http://www.xbrl.org/2003/iso4217""
            xmlns:ffiec031-2007-06-30=""http://www.ffiec.gov/xbrl/call/report031/2007-06-30/v34""
            xmlns:cc=""http://www.ffiec.gov/xbrl/call/concepts""
            xsi:schemaLocation=""http://www.ffiec.gov/xbrl/call/report031/2007-06-30/v34/
            call-report031-2007-06-30-v34.xsd http://www.ffiec.gov/xbrl/call/concepts
            concepts.xsd"">
                <cc:RCON1480 contextRef=""CI_1402629_2003-03-31"" unitRef=""USD"" decimals=""0"">34984000</cc:RCON1480>
                <cc:RCON4088 contextRef=""CI_2631314_2005-03-31"">true</cc:RCON4088>
                <cc:RCONF233 contextRef=""CI_141958_2021-12-31"" unitRef=""USD"" decimals=""0"">0</cc:RCONF233>
                <cc:RCONF236 contextRef=""CI_141958_2021-12-31"" unitRef=""USD"" decimals=""0"">101098000</cc:RCONF236>
                <cc:RCONF237 contextRef=""CI_141958_2021-12-31"" unitRef=""USD"" decimals=""0"">0</cc:RCONF237>
                <cc:RCONB883 contextRef=""CI_141958_2021-12-31"" unitRef=""NON-MONETARY"" decimals=""0"">0</cc:RCONB883>
                <cc:RCONB557 contextRef=""CI_141958_2021-12-31"" unitRef=""USD"" decimals=""0"">0</cc:RCONB557>
                <cc:RCON6860 contextRef=""CI_141958_2021-12-31"">true</cc:RCON6860>
                <cc:RCONS452 contextRef=""CI_141958_2021-12-31"" unitRef=""USD"" decimals=""0"">0</cc:RCONS452>
                <cc:RCONK132 contextRef=""CI_141958_2021-12-31"" unitRef=""USD"" decimals=""0"">0</cc:RCONK132>
                <cc:RCONK131 contextRef=""CI_141958_2021-12-31"" unitRef=""USD"" decimals=""0"">0</cc:RCONK131>
                <cc:RCONHT61 contextRef=""CI_141958_2020-03-31"" unitRef=""USD"" decimals=""0"">0</cc:RCONHT61>
                <cc:RCONHT60 contextRef=""CI_141958_2020-03-31"" unitRef=""USD"" decimals=""0"">0</cc:RCONHT60>
                <cc:RCONJ459 contextRef=""CI_141958_2020-03-31"" unitRef=""USD"" decimals=""0"">833000</cc:RCONJ459>
                <cc:RCONJ458 contextRef=""CI_141958_2020-03-31"" unitRef=""USD"" decimals=""0"">0</cc:RCONJ458>
                <cc:RCONG320 contextRef=""CI_141958_2020-03-31"" unitRef=""USD"" decimals=""0"">0</cc:RCONG320>
                <cc:RCONG321 contextRef=""CI_141958_2020-03-31"" unitRef=""USD"" decimals=""0"">0</cc:RCONG321>
                <cc:RCONG322 contextRef=""CI_141958_2020-03-31"" unitRef=""USD"" decimals=""0"">0</cc:RCONG322>
                <cc:RCONG323 contextRef=""CI_141958_2020-03-31"" unitRef=""USD"" decimals=""0"">0</cc:RCONG323>
                <cc:RCON3506 contextRef=""CI_141958_2020-03-31"" unitRef=""USD"" decimals=""0"">0</cc:RCON3506>
                <cc:RCONKX18 contextRef=""CI_141958_2020-03-31"">false</cc:RCONKX18>
            </xbrl>"; 
    }
}
