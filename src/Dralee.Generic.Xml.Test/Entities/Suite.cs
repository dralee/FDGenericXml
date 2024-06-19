namespace Dralee.Generic.Xml.Test.Entities;

public class Suite
{
    public string SuiteId { get; set; }
    public string AuthCode { get; set; }
    public string InfoType { get; set; }
    public int TimeStamp { get; set; }
    public string State { get; set; }
    public string ExtraInfo { get; set; }

    public override string ToString()
    {
        return
            $"{nameof(SuiteId)}:{SuiteId}," +
            $"{nameof(AuthCode)}:{AuthCode}," +
            $"{nameof(InfoType)}:{InfoType}," +
            $"{nameof(TimeStamp)}:{TimeStamp}," +
            $"{nameof(State)}:{State}," +
            $"{nameof(ExtraInfo)}:{ExtraInfo}";
    }
}