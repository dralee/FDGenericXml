using System;
using System.Text;

namespace Dralee.Generic.Xml.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var pt = new PersonTest();
            pt.Test();
            
            pt.Test2();

            Console.Read();
        }
    }
}