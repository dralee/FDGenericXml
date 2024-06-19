using Dralee.Generic.Xml.Test.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dralee.Generic.Xml.Test
{
    // CreatedBy: Jackie Lee（天宇遊龍）
    // CreatedOn: 2017-04-13
    // 博客：http://www.cnblogs.com/dralee
    /// <summary>
    /// 测试类
    /// </summary>
    public class PersonTest
    {
        public void Test()
        {
            Person p1 = new Person
            {
                Id = 1,
                Name = "Jackie",
                Gender = Gender.Male,
                Phone = "18412345678",
                Address = new Address { Province = "广东", City = "深圳", Detail = "xx区xx街道xxxx号" }
            };
            Person p2 = new Person
            {
                Id = 2,
                Name = "Hony",
                Gender = Gender.Female,
                Phone = "13512345678",
                Address = new Address { Province = "广东", City = "深圳", Detail = "yy区yy街道yyyy号" }
            };

            XmlSerializer<Person> xs = new XmlSerializer<Person>("<?xml version=\"1.0\" encoding=\"utf-8\"?>",true);
            var xml1 = xs.ToXml(p1);
            OutPrint("对象序列化", xml1);
            var xml2 = xs.ToXml(p2);
            OutPrint("xml反序列化", xml2);

            var pp = xs.FromXml(xml1);

            Console.WriteLine("\r\n============= 数组对象 ===============");

            var pArr = new Person[] { p1, p2 };
            XmlSerializer<Person[]> xsArr = new XmlSerializer<Person[]>("");
            var xml4 = xsArr.ToXml(pArr);
            OutPrint("数组对象序列化", xml4);

            var pArr2 = xsArr.FromXml(xml4);
            Console.WriteLine("============= 数组对象反序列化 ===============");
            pArr2.ToList().ForEach(p =>
            {
                OutPrint("数组对象", p.ToString());
            });

            Console.WriteLine("\r\n============= 泛型集合对象 ===============");
            var ps = new List<Person> { p1, p2 };
            XmlSerializer<List<Person>> xsList = new XmlSerializer<List<Person>>("");
            var xml3 = xsList.ToXml(ps);
            OutPrint("泛型集合对象序列化", xml3);

            var ps2 = xsList.FromXml(xml3);
            Console.WriteLine("============= 泛型集合对象反序列化 ===============");
            ps2.ForEach(p =>
            {
                OutPrint("泛型集合对象", p.ToString());
            });
        }

        public void Test2()
        {
var suite = new Suite
{
    SuiteId = "ww213412348923",
    AuthCode = "AUTHCODE",
    InfoType = "create_auth",
    TimeStamp = 1603610513,
    State = "123458",
    ExtraInfo = "h"
};
Console.WriteLine("\n========================================== suite ===================");
var xs = new XmlSerializer<Suite>("<?xml version=\"1.0\" encoding=\"utf-8\"?>",CDataFormatFor.String);
var xml1 = xs.ToXml(suite);
OutPrint("对象序列化 string", xml1);
var suite1 = xs.FromXml(xml1);
OutPrint("xml反序列化 string", suite1.ToString());

var xs2 = new XmlSerializer<Suite>("<?xml version=\"1.0\" encoding=\"utf-8\"?>",CDataFormatFor.None);
var xml2 = xs2.ToXml(suite);
OutPrint("对象序列化 none", xml2);
var suite2 = xs2.FromXml(xml2);
OutPrint("xml反序列化 none", suite2.ToString());

var xs3 = new XmlSerializer<Suite>("<?xml version=\"1.0\" encoding=\"utf-8\"?>",CDataFormatFor.All);
var xml3 = xs3.ToXml(suite);
OutPrint("对象序列化 all", xml3);
var suite3 = xs3.FromXml(xml3);
OutPrint("xml反序列化 all", suite3.ToString());
        }

        private void OutPrint(string tip, string msg)
        {
            Console.WriteLine("======>{0}：", tip);
            Console.WriteLine(msg);
        }
    }
}
