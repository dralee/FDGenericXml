using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FD.Generic.Xml.Regexs
{
    // CreatedBy: Jackie Lee（天宇遊龍）
    // CreatedOn: 2017-04-13
    // 博客：http://www.cnblogs.com/dralee
    /// <summary>
    /// Xml标签帮助
    /// </summary>
    public class XmlTagHelper
    {
        /// <summary>  
        /// 获取字符中指定标签的值  
        /// </summary>  
        /// <param name="content">字符串</param>  
        /// <param name="tagName">标签</param>  
        /// <param name="attrib">属性名</param>  
        /// <returns>属性</returns>  
        public static string GetTagContent(string content, string tagName, string attrib)
        {
            string tmpStr = string.IsNullOrEmpty(attrib) ? $"<{tagName}>([\\s\\S]*?)</{tagName}>" :
                $"<{tagName}\\s*{attrib}\\s*=\\s*.*?>([\\s\\S]*?)</{tagName}>";
            Match match = Regex.Match(content, tmpStr, RegexOptions.IgnoreCase);

            string result = match.Groups[1].Value;
            return result;
        }

        /// <summary>  
        /// 获取字符中指定标签的值  
        /// </summary>  
        /// <param name="content">字符串</param>  
        /// <param name="tagName">标签</param>  
        /// <param name="attrib">属性名</param>  
        /// <returns>属性</returns>  
        public static List<string> GetTagContents(string content, string tagName, string attrib)
        {
            string tmpStr = string.IsNullOrEmpty(attrib) ? $"<{tagName}>([\\s\\S]*?)</{tagName}>" :
                $"<{tagName}\\s*{attrib}\\s*=\\s*.*?>([\\s\\S]*?)</{tagName}>";
            MatchCollection matchs = Regex.Matches(content, tmpStr, RegexOptions.IgnoreCase);

            var result = new List<string>();
            foreach (Match match in matchs)
            {
                result.Add(match.Groups[1].Value);
            }
            return result;
        }
    }
}
