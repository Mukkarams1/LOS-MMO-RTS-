using System.Collections.Generic;
using System;

public class Building
{
    public string _id { get; set; }
    public string buildingId { get; set; }
    public int currentLevel { get; set; }
    public DateTime requiredTime { get; set; }
    public List<Coordinate> coordinates { get; set; }
    public bool upgraded { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
}
public class Coordinate
{
    public int x { get; set; }
    public int y { get; set; }
}

public class MainInventory
{
    public int food { get; set; }
    public int wood { get; set; }
    public int metal { get; set; }
    public int gunPowder { get; set; }
    public int diamond { get; set; }
    public int gold { get; set; }
}

public class Data
{
    public string _id { get; set; }
    public string role { get; set; }
    public string userName { get; set; }
    public string deviceToken { get; set; }
    public List<Coordinate> coordinates { get; set; }
    public bool recruitmentInProgress { get; set; }
    public bool buildingInProgress { get; set; }
    public bool isPremium { get; set; }
    public bool isVip { get; set; }
    public bool isGuest { get; set; }
    public int overallPower { get; set; }
    public int level { get; set; }
    public int currentChapterProgress { get; set; }
    public MainInventory mainInventory { get; set; }
    public List<WarehouseInventory> warehouseInventory { get; set; }
    public List<Troop> troops { get; set; }
    public List<Building> buildings { get; set; }
    public List<Mail> mails { get; set; }
    public List<Gift> gifts { get; set; }

    public string twitterId { get; set; }
    public string twitterProfileImageUrlHttps { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public int __v { get; set; }
    public List<ChapterProgress> chapterProgress { get; set; }
    public List<Booster> boosters { get; set; }
    public DateTime serverTime { get; set; }
}
public class ChapterProgress
{
    public string _id { get; set; }
    public List<ChapterProgressTasks> tasks { get; set; }
}
public class ChapterProgressTasks
{
    public string taskId { get; set; }
    public bool redeem { get; set; }
    public string _id { get; set; }
}
public class WarehouseInventory
{
    public string _id { get; set; }
    public string imagePath { get; set; }
    public string title { get; set; }
    public int quantity { get; set; }
    public string description { get; set; }
    public int unitValue { get; set; }
    public int redeemCategory { get; set; }
    public int inventoryType { get; set; }
}

public class Warrior
{
    public string _id { get; set; }
    public string warriorId { get; set; }
    public int quantity { get; set; }
}
public class Mail
{
    public string _id { get; set; }
    public string from { get; set; }
    public string to { get; set; }
    public string subject { get; set; }
    public string description { get; set; }
    public string link { get; set; }
    public bool isRead { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
}

public class SignInResponse
{
    public int code { get; set; }
    public bool status { get; set; }
    public string msg { get; set; }
    public Data data { get; set; }
    public string accessToken { get; set; }
    public string refreshToken { get; set; }
}


public class Troop
{
    public string _id { get; set; }
    public string troopId { get; set; }
    public int quantity { get; set; }
    public DateTime requiredTime { get; set; }
    public int addedQuantity { get; set; }
}

public class Gift
{
    public string _id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public List<GiftInventory> inventory { get; set; }
}
public class GiftInventory
{
    public string _id { get; set; }
    public string title { get; set; }
    public string unitValue { get; set; }
    public string quantity { get; set; }
    public string redeemCategory { get; set; }
    public string inventoryType { get; set; }
    public string imagePath { get; set; }
    public string description { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
}
public class Booster
{
    public string _id { get; set; }
    public int inventoryType { get; set; }
    public int boostFactor { get; set; }
    public DateTime lastResourcesGathered { get; set; }
    public DateTime expiry { get; set; }
}


public class RefreshTokenResponse
{
    public int code { get; set; }
    public bool status { get; set; }
    public string msg { get; set; }
    public string accessToken { get; set; }
    public string refreshToken { get; set; }
}

public class GenericErrorResopnse
{
    public int code { get; set; }
    public string msg { get; set; }
    public bool status { get; set; }
}