using System;

namespace FD.Generic.Xml.Exceptions
{
    // CreatedBy: Jackie Lee（天宇遊龍）
    // CreatedOn: 2017-04-13
    // 博客：http://www.cnblogs.com/dralee
    /// <summary>
    /// XML序列化异常
    /// </summary>
    public class XmlSerializerException : Exception
    {
        public XmlSerializerException() { }
        public XmlSerializerException(string message) : base(message)
        {
        }
        public XmlSerializerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
