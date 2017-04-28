# FDGenericXml
.net core xml serializer

### How to use the library?(details for http://www.cnblogs.com/dralee/p/6705398.html)

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
