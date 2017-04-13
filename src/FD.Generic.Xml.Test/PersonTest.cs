using FD.Generic.Xml.Test.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FD.Generic.Xml.Test
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

            XmlSerializer<Person> xs = new XmlSerializer<Person>("<?xml version=\"1.0\" encoding=\"utf - 8\"?>");
            var xml1 = xs.ToXml(p1);
            OutPrint("对象序列化", xml1);
            var xml2 = xs.ToXml(p2);
            OutPrint("xml反序列化", xml2);

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

        private void OutPrint(string tip, string msg)
        {
            Console.WriteLine("======>{0}：", tip);
            Console.WriteLine(msg);
        }
    }
}
