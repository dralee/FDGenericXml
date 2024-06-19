namespace Dralee.Generic.Xml.Test.Entities
{
    // CreatedBy: Jackie Lee（天宇遊龍）
    // CreatedOn: 2017-04-13
    // 博客：http://www.cnblogs.com/dralee
    /// <summary>
    /// 人员
    /// </summary>
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public Gender Gender { get; set; }

        public override string ToString()
        {
            return $"{Id}:{Name},{Gender},{Phone},{Address}";
        }
    }
}
