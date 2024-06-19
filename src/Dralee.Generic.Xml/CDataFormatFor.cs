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

    /// <summary>
    /// 兼容性
    /// </summary>
    public enum CompatibleLevel
    {
        /// <summary>
        /// 严格
        /// </summary>
        Strict,
        
        /// <summary>
        /// 兼容，允许根不一致，内容大致一致
        /// </summary>
        Compatible
    }
}