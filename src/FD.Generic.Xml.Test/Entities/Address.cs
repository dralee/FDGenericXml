namespace FD.Generic.Xml.Test.Entities
{
    // CreatedBy: Jackie Lee（天宇遊龍）
    // CreatedOn: 2017-04-13
    // 博客：http://www.cnblogs.com/dralee
    /// <summary>
    /// 地址
    /// </summary>
    public class Address
    {
        public string Province { get; set; }
        public string City { get; set; }
        public string Detail { get; set; }

        public override string ToString()
        {
            return $"from:{Province}，{City}，{Detail}";
        }
    }
}
