namespace FD.Generic.Xml.Extensions
{
    // CreatedBy: Jackie Lee（天宇遊龍）
    // CreatedOn: 2017-04-13
    // 博客：http://www.cnblogs.com/dralee
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string FirstToLower(this string word)
        {
            if (string.IsNullOrEmpty(word))
                return string.Empty;
            return word.Substring(0, 1).ToLower() + word.Substring(1);
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string FirstToUpper(this string word)
        {
            if (string.IsNullOrEmpty(word))
                return string.Empty;
            return word.Substring(0, 1).ToUpper() + word.Substring(1);
        }
    }
}
