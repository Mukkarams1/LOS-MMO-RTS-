using System;
using System.Collections.Generic;

public class CostPerUnit
{
    public int food { get; set; }
    public int wood { get; set; }
    public int metal { get; set; }
    public int gunPowder { get; set; }
    public int diamond { get; set; }
    public int gold { get; set; }
    public int time { get; set; }
}
public class TroopsData
{
    public CostPerUnit costPerUnit { get; set; }
    public string _id { get; set; }
    public string title { get; set; }
    public string shortDescription { get; set; }
    public string description { get; set; }
    public string image { get; set; }
    public int unlockLevel { get; set; }
    public int powerPerWarrior { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public int __v { get; set; }
}


public class TroopsResponseData
{
    public int code { get; set; }
    public bool status { get; set; }
    public string msg { get; set; }
    public List<TroopsData> data { get; set; }
}