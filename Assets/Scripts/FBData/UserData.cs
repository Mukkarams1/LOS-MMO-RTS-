
// using UnityEngine;
// using Firebase.Firestore;


// [FirestoreData]
// public struct User
// {
//     [FirestoreProperty]
//     public string _id { get; set; }

//     [FirestoreProperty]
//     public UserDetails UserDetails { get; set; }

//     [FirestoreProperty]
//     public MainInventory MainInventory { get; set; }

// [FirestoreProperty]
// public WarehouseInventory WarehouseInventory { get; set; }

// [FirestoreProperty]
// public Warriors Warriors { get; set; }

// [FirestoreProperty]
// public Buildings Buildings { get; set; }

// [FirestoreProperty]
// public float Is_premium { get; set; }

// [FirestoreProperty]
// public string Is_vip { get; set; }

// [FirestoreProperty]
// public string Is_guest { get; set; }

// }


// [FirestoreData]
// public struct UserDetails
// {
//     [FirestoreProperty]
//     public string FirstName { get; set; }

//     [FirestoreProperty]
//     public string LastName { get; set; }

//     [FirestoreProperty]
//     public string Email { get; set; }

//     [FirestoreProperty]
//     public string PhoneNumber { get; set; }

//     [FirestoreProperty]
//     public string GuestName { get; set; }

//     [FirestoreProperty]
//     public string NickName { get; set; }

//     [FirestoreProperty]
//     public float TimeZone { get; set; }

//     [FirestoreProperty]
//     public string CityId { get; set; }

//     [FirestoreProperty]
//     public string Coordinates { get; set; }

// }

// [FirestoreData]
// public struct MainInventory
// {
//     [FirestoreProperty]
//     public ulong Food { get; set; }

//     [FirestoreProperty]
//     public ulong Wood { get; set; }

//     [FirestoreProperty]
//     public ulong Metal { get; set; }

//     [FirestoreProperty]
//     public ulong Gunpowder { get; set; }

//     [FirestoreProperty]
//     public ulong Diamond { get; set; }

//     [FirestoreProperty]
//     public ulong Gold { get; set; }

// }

// [FirestoreData]
// public struct WarehouseInventory
// {
//     [FirestoreProperty]
//     public ulong Food { get; set; }

//     [FirestoreProperty]
//     public ulong Wood { get; set; }

//     [FirestoreProperty]
//     public ulong Metal { get; set; }

//     [FirestoreProperty]
//     public ulong Gunpowder { get; set; }

//     [FirestoreProperty]
//     public ulong Diamond { get; set; }

//     [FirestoreProperty]
//     public ulong Gold { get; set; }

//     [FirestoreProperty]
//     public ulong SpeedUp { get; set; }

//     [FirestoreProperty]
//     public ulong ProductionSpeedUp { get; set; }

// }

// [FirestoreData]
// public struct Warriors
// {
//     [FirestoreProperty]
//     public string Id { get; set; }

//     [FirestoreProperty]
//     public string WarriorId { get; set; }

//     [FirestoreProperty]
//     public ulong Quantity { get; set; }

// }

// public struct Buildings
// {
//     [FirestoreProperty]
//     public string Id { get; set; }

//     [FirestoreProperty]
//     public string BuildingId { get; set; }

//     [FirestoreProperty]
//     public int CurrentLevel { get; set; }

//     [FirestoreProperty]
//     public int position { get; set; }

// }