namespace Dralee.Generic.Xml
{
    /// <summary>
    /// cdata类型值包裹
    /// </summary>
    public enum CDataFormatFor
    {
        /// <summary>
        /// 不需要cdata类型值包裹
        /// </summary>
        None,
        
        /// <summary>
        /// 只有string类型值需要包裹
        /// </summary>
        String,
        
        /// <summary>
        /// 所以类型值都需要包裹
        /// </summary>
        All
    }
}