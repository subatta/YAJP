using System;
using System.Web.Script.Serialization;

namespace ConsoleApplication2
{
    using FormDataToJson;

    class Program
    {
        static void Main(string[] args)
        {
            var s = "Key1.A.B=Value1AB&Key1.A.C=Value1AC&Key2.A_1.A1Key=Value2A1&Key2.A_2.A2Key=Value2A2&Key3=Value3";

            var json = new Parser().JsonFromString(s);
            Console.WriteLine(json);

            var x = new JavaScriptSerializer().Deserialize<object>(json);

        }
    }
}
