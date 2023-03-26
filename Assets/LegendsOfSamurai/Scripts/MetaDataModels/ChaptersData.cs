using System;
using System.Collections.Generic;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class ChapterGift
{
    public string giftTitle { get; set; }
    public string giftDescription { get; set; }
    public int food { get; set; }
    public int wood { get; set; }
    public int metal { get; set; }
    public int gunPowder { get; set; }
    public int diamond { get; set; }
    public int gold { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
}
public class Criterion
{
    public string type { get; set; }
    public string itemID { get; set; }
    public int level { get; set; }
    public int quantity { get; set; }
}

public class ChapterData
{
    public string _id { get; set; }
    public int chapterNumber { get; set; }
    public string chapterTitle { get; set; }
    public string chapterDescription { get; set; }
    public string chapterShortDescription { get; set; }
    public List<ChapterGift> chapterGift { get; set; }
    public List<ChapterTask> tasks { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public int __v { get; set; }
}

public class ChapterDataResponse
{
    public int code { get; set; }
    public bool status { get; set; }
    public string msg { get; set; }
    public List<ChapterData> data { get; set; }
}

public class ChapterTask
{
    public Criterion criteriaNew { get; set; }
    public string _id { get; set; }
    public int taskNumber { get; set; }
    public string taskTitle { get; set; }
    public List<TaskGift> taskGifts { get; set; }
}

public class TaskGift
{
    public string giftTitle { get; set; }
    public string giftDescription { get; set; }
    public int food { get; set; }
    public int wood { get; set; }
    public int metal { get; set; }
    public int gunPowder { get; set; }
    public int diamond { get; set; }
    public int gold { get; set; }
}


public enum TaskCriteriaItemsEnum
{
    food,
    wood,
    metal,
    gunPowder,
    diamond,
    gold,
    building,
    troop
}