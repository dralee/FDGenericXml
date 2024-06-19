### .net core xml serializer
#### 功能：
1. 将一个普通对象序列化为一个xml，及将其对应的xml格式反序列化为该对象；
2. 将一个数组集合对象序列化为一个xml，及将其对应的xml格式反序列化为该对象；
3. 将一个泛型集合对象序列化为一个xml，及将其对应的xml格式反序列化为该对象；

#### 使用：
```csharp
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
```

### nuget 使用方式
```bash
dotnet add package Dralee.Generic.Xml --version 1.0.3
```