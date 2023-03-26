using System;
using System.Collections.Generic;

public class CostPerLevel
{
    public string _id { get; set; }
    public int levelNumber { get; set; }
    public int food { get; set; }
    public int wood { get; set; }
    public int metal { get; set; }
    public int gunPowder { get; set; }
    public int diamond { get; set; }
    public int gold { get; set; }
    public int time { get; set; }
    public int yieldPerHour { get; set; }
    public int power { get; set; }
}

public class BuildingsData
{
    public string _id { get; set; }
    public string title { get; set; }
    public string image { get; set; }
    public string shortDescription { get; set; }
    public string description { get; set; }
    public int category { get; set; }
    public int unlockLevel { get; set; }
    public int maxLimit { get; set; }
    public int xSize { get; set; }
    public int ySize { get; set; }
    public List<CostPerLevel> costPerLevel { get; set; }
    public int yieldCategory { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public int __v { get; set; }
}

public class BuildingsDataResponse
{
    public int code { get; set; }
    public bool status { get; set; }
    public string msg { get; set; }
    public List<BuildingsData> data { get; set; }
}
