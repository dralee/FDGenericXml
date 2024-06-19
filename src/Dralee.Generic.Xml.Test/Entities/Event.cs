namespace Dralee.Generic.Xml.Test.Entities;

public class Event
{
    public string AuthCode { get; set; }
    public string InfoType { get; set; }
    public int TimeStamp { get; set; }
    
    public override string ToString()
    {
        return
            $"{nameof(AuthCode)}:{AuthCode}," +
            $"{nameof(InfoType)}:{InfoType}," +
            $"{nameof(TimeStamp)}:{TimeStamp},";
    }
}