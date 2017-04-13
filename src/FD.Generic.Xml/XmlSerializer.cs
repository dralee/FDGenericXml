using FD.Generic.Xml.Exceptions;
using FD.Generic.Xml.Extensions;
using FD.Generic.Xml.Regexs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace FD.Generic.Xml
{
    // CreatedBy: Jackie Lee（天宇遊龍）
    // CreatedOn: 2017-04-13
    // 博客：http://www.cnblogs.com/dralee
    /// <summary>
    /// Xml序列及反序列化操作
    /// </summary>
    public class XmlSerializer<T>
    {
        private string _xmlHead;
        private string _rootTag;
        private ElementType _elemType;
        private Type _elementType;

        /// <summary>
        /// Xml序列及反序列化操作
        /// </summary>
        /// <param name="xmlHead">XML文件头<?xml ... ?></param>
        /// <param name="rootTag">根标签名称</param>
        public XmlSerializer(string xmlHead)
        {
            _xmlHead = xmlHead;
            if (typeof(T).GetTypeInfo().IsGenericType)
            {
                _elemType = ElementType.Generic;
                _elementType = typeof(T).GenericTypeArguments.FirstOrDefault();
                _rootTag = _elementType.Name;
            }
            else if (typeof(T).GetTypeInfo().IsArray)
            {
                _elemType = ElementType.Array;
                _elementType = typeof(T).GetTypeInfo().GetElementType();
                _rootTag = _elementType.Name;
            }
            else
            {
                _rootTag = typeof(T).Name;
            }
        }

        #region 对象转xml
        /// <summary>
        /// 序列化报文为xml
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public string ToXml(T packet)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(_xmlHead))
            {
                sb.AppendFormat("{0}\r\n", _xmlHead);
            }
            try
            {
                Visit(sb, packet);
            }
            catch (Exception ex)
            {
                throw new XmlSerializerException($"序列化对象异常:{ex.Message}", ex);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 访问对象入口
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="obj"></param>
        private void Visit(StringBuilder sb, object obj)
        {
            if (obj is IEnumerable)
            {
                sb.AppendFormat("<{0}s>", _rootTag);
                VisitCollection(sb, (IEnumerable)obj);
                sb.AppendFormat("</{0}s>", _rootTag);
            }
            else
            {
                VisitObject(sb, obj);
            }
        }

        /// <summary>
        /// 访问集合
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="obj"></param>
        private void VisitCollection(StringBuilder sb, IEnumerable obj)
        {
            foreach (var item in obj)
            {
                if (item is Enumerable)
                {
                    VisitCollection(sb, (IEnumerable)item);
                }
                else
                {
                    VisitObject(sb, item);
                }
            }
        }

        /// <summary>
        /// 访问对象
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="packet"></param>
        private void VisitObject(StringBuilder sb, object packet)
        {
            var pFields = packet.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (pFields.Count() > 0)
            {
                sb.AppendFormat("<{0}>", _rootTag);
                VisitFields(sb, packet, pFields);
                sb.AppendFormat("</{0}>", _rootTag);
            }
        }

        /// <summary>
        /// 访问属性
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="obj"></param>
        /// <param name="fields"></param>
        /// <param name="exceptFields"></param>
        private void VisitFields(StringBuilder sb, object obj, PropertyInfo[] fields, params string[] exceptFields)
        {
            foreach (var field in fields)
            {
                if (exceptFields != null && exceptFields.Contains(field.Name))
                    continue;
                sb.AppendFormat("<{0}>", field.Name.FirstToLower());

                if (!field.PropertyType.FullName.StartsWith("System."))
                {
                    object subObj = field.GetValue(obj);
                    var subFields = field.PropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    if (subFields.Count() > 0)
                    {
                        VisitFields(sb, subObj, subFields);
                    }
                    else
                    {
                        sb.Append(field.GetValue(obj));
                    }
                }
                else
                {
                    sb.Append(field.GetValue(obj));
                }
                sb.AppendFormat("</{0}>", field.Name.FirstToLower());
            }
        }
        #endregion

        #region xml转对象
        /// <summary>
        /// 序列化为报文内容
        /// </summary>
        /// <param name="xml">以<packet>标签开始的xml内容</param>
        /// <returns></returns>
        public T FromXml(string xml)
        {
            int index;
            if (xml.Trim().StartsWith("<?xml") && (index = xml.IndexOf("?>")) != -1)
            {
                xml = xml.Substring(index + 2).Trim('\r', '\n', ' ');
            }
            try
            {
                switch (_elemType)
                {
                    case ElementType.Generic:
                        return VisitXmlGeneric(xml);
                    case ElementType.Array:
                        return VisitXmlArray(xml);
                    default:
                        return VisitXmlObject(xml);
                }
            }
            catch (Exception ex)
            {
                throw new XmlSerializerException($"反序列化对象信息异常:{ex.Message}", ex);
            }
        }

        /// <summary>
        /// 访问xml中对象集合
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private T VisitXmlGeneric(string xml)
        {
            T collection = Activator.CreateInstance<T>();
            List<string> xmlArr = XmlTagHelper.GetTagContents(xml, _rootTag, "");
            foreach (var itemXml in xmlArr)
            {
                AddElement(collection, itemXml, obj =>
                {
                    Add(collection, obj);
                });
            }
            return collection;
        }

        /// <summary>
        /// 访问xml中对象集合
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private T VisitXmlArray(string xml)
        {
            List<string> xmlArr = XmlTagHelper.GetTagContents(xml, _rootTag, "");
            Array array = Array.CreateInstance(_elementType, xmlArr.Count);
            T collection = (T)Convert.ChangeType(array, typeof(T));
            int index = 0;
            foreach (var itemXml in xmlArr)
            {
                AddElement(collection, itemXml, obj =>
                {
                    SetValue(collection, obj, index++);
                });
            }
            return collection;
        }

        /// <summary>
        /// 添加元素到集合
        /// </summary>
        /// <param name="collection">集合</param>
        /// <param name="itemXml">元素xml</param>
        /// <param name="addItem">集合项添加操作</param>
        private void AddElement(T collection, string itemXml, Action<object> addItem)
        {
            var obj = Activator.CreateInstance(_elementType);
            VisitXml($"<{_rootTag}>{itemXml}</{_rootTag}>", obj, _elementType.GetProperties(BindingFlags.Instance | BindingFlags.Public));
            addItem(obj);
        }

        /// <summary>
        /// 访问xml对象
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private T VisitXmlObject(string xml)
        {
            if (string.IsNullOrEmpty(xml) || !xml.StartsWith($"<{_rootTag}>"))
            {
                throw new XmlSerializerException($"反序列化对象信息异常:指定xml内容与指定对象类型{typeof(T)}不匹配");
            }
            T packet = Activator.CreateInstance<T>();
            VisitXml(xml, packet, typeof(T).GetProperties());
            return packet;
        }

        /// <summary>
        /// 添加元素到集合中
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="obj"></param>
        private void Add(T collection, object obj)
        {
            var methodInfo = typeof(T).GetMethods(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(m => m.Name.Equals("Add"));
            if (methodInfo == null)
            {
                throw new XmlSerializerException($"反序列化集合xml内容失败，目标{typeof(T).FullName}非集合类型");
            }
            var instance = Expression.Constant(collection);
            var param = Expression.Constant(obj);
            var addExpression = Expression.Call(instance, methodInfo, param);
            var add = Expression.Lambda<Action>(addExpression).Compile();
            add.Invoke();
        }

        /// <summary>
        /// 添加元素到集合中
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="obj"></param>
        private void SetValue(T collection, object obj, int index)
        {
            var methodInfo = typeof(T).GetMethods(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(m => m.Name.Equals("SetValue"));
            if (methodInfo == null)
            {
                throw new XmlSerializerException($"反序列化集合xml内容失败，目标{typeof(T).FullName}非集合类型");
            }
            var instance = Expression.Constant(collection);
            var param1 = Expression.Constant(obj);
            var param2 = Expression.Constant(index);
            var addExpression = Expression.Call(instance, methodInfo, param1, param2);
            var setValue = Expression.Lambda<Action>(addExpression).Compile();
            setValue.Invoke();
        }

        /// <summary>
        /// 对象序列化为xml
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="obj"></param>
        /// <param name="fields"></param>
        private void VisitXml(string xml, object obj, PropertyInfo[] fields)
        {
            foreach (var field in fields)
            {
                Type subType = field.PropertyType;
                if (!subType.FullName.StartsWith("System.") && !IsEnumType(subType))
                {
                    object subObj = Activator.CreateInstance(subType);// field.GetValue(obj);
                    var subFields = subType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    field.SetValue(obj, subObj);
                    if (subFields.Count() > 0)
                    {
                        VisitXml(xml, subObj, subFields);
                    }
                    else
                    {
                        field.SetValue(subObj, XmlTagHelper.GetTagContent(xml, field.Name.FirstToLower(), ""));
                    }
                }
                else
                {
                    var value = XmlTagHelper.GetTagContent(xml, field.Name.FirstToLower(), "");
                    if (subType != typeof(string))
                    {
                        if (IsEnumType(subType))
                        {
                            field.SetValue(obj, Enum.Parse(subType, value));
                        }
                        else
                        {
                            field.SetValue(obj, Convert.ChangeType(value, subType));
                        }
                    }
                    else
                    {
                        field.SetValue(obj, value);
                    }
                }
            }
        }
        #endregion

        #region 帮助项
        /// <summary>
        /// 是否为枚举类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool IsEnumType(Type type)
        {
            return type.GetTypeInfo().IsEnum;
        }

        /// <summary>
        /// 元素类型
        /// </summary>
        private enum ElementType
        {
            Object, Array, Generic
        }
        #endregion
    }
}
