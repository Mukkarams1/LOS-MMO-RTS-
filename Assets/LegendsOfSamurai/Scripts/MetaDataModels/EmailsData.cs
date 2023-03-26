public class EmailsData
{
    public string from { get; set; }
    public string to { get; set; }
    public string subject { get; set; }
    public string description { get; set; }
    public string link { get; set; }
    public bool isRead { get; set; }
    public string createdAt { get; set; }
}


public class EmailUpdateResponse
{
    public int code { get; set; }
    public bool status { get; set; }
    public string msg { get; set; }
    public string data { get; set; }
}
