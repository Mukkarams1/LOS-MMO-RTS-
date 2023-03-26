using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static Action onUserDataUpdated;



    

    public static DataManager instance;
    public List<Data> userData;
    public List<TroopsData> troopData;
    public List<BuildingsData> buildingsData;
    public List<EmailsData> emailsData;
    public ChatData chatData;
    public TextAsset userdatajson;
    public TextAsset Troopdatajson;
    public TextAsset Buildingdatajson;
    public TextAsset Emaildatajson;
    public TextAsset Characterdatajson;
    public TextAsset Chatdatajson;

    #region images
    public Sprite foodBundleSprite;
    public Sprite woodBundleSprite;
    public Sprite metalBundleSprite;
    public Sprite gunPowderBundleSprite;
    public Sprite goldBundleSprite;
    public Sprite diamondBundleSprite;
    public Sprite boosterBundleSprite;
    public Sprite speedUpBundleSprite;

    #endregion

    public SignInResponse SignInResponseData { get  ; private set; }  
    private BuildingsDataResponse buildingsDataResponse = new BuildingsDataResponse();
    public List<BuildingsData> allBuildingsDataList;
    private TroopsResponseData troopsResponseData = new TroopsResponseData();
    public List<TroopsData> allTroopsDataList;

    private ChapterDataResponse chapterResponseData = new ChapterDataResponse();
    List<ChapterData> allChapterDataList = new List<ChapterData>();


    private List<Building> userBuildingsDataList = new List<Building>();
    private List<Mail> userMailsDataList = new List<Mail>();
    private List<WarehouseInventory> userWareHouseInventoryDataList = new List<WarehouseInventory>();
    private List<Troop> userTroopsDataList = new List<Troop>();
    private MainInventory userMainInventoryData = new MainInventory();
    private List<ChapterProgress> userChaptersProgressList = new List<ChapterProgress>();
    private List<Gift> userGiftsList = new List<Gift>();
    private List<Booster> userAppliedBoostersList = new List<Booster>();
    private void Awake()
    {
        DontDestroyOnLoad();
        if (instance == null)
            instance = this;
        SignInResponseData = new SignInResponse();

       

    }

    private void Start()
    {


    }
    public void FetchAllBuildingsData()
    {
        ApiController.ListAllBuildingsAPI(ListAllBuildingCallBack, SignInWithTwitter.losAcessToken);
    }

    public List<Building> GetUserBuildingsData()
    {
        return userBuildingsDataList;
    }
    public List<Booster> GetUserAppliedBoosters()
    {
        return userAppliedBoostersList;
    }
    public void SetUserData(SignInResponse signInResponse) 
    {
        SignInResponseData = signInResponse;
        userBuildingsDataList = signInResponse.data.buildings;
        userMainInventoryData = signInResponse.data.mainInventory;
        userMailsDataList = signInResponse.data.mails;
        userWareHouseInventoryDataList = signInResponse.data.warehouseInventory;
        userTroopsDataList = signInResponse.data.troops;
        userChaptersProgressList = signInResponse.data.chapterProgress;
        userGiftsList = signInResponse.data.gifts;
        userAppliedBoostersList = signInResponse.data.boosters;
    }

    void DontDestroyOnLoad()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);

        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ListAllBuildingCallBack(bool status, String response)
    {
        if ((!status))
        {
            return;
        }
        else
        {
            buildingsDataResponse = JsonConvert.DeserializeObject<BuildingsDataResponse>(response);
            allBuildingsDataList = buildingsDataResponse.data;
            Debug.Log("allBuildingsDataList loaded " + buildingsDataResponse.data.Count);
        }

    }
    public void FetchAllChaptersData()
    {
        ApiController.GetAllChaptersAPI(AllChaptersAPICallBack, SignInWithTwitter.losAcessToken);
    }
    void AllChaptersAPICallBack(bool status, string resp)
    {
        if ((!status))
        {
            return;
        }
        else
        {
            chapterResponseData = JsonConvert.DeserializeObject<ChapterDataResponse>(resp);
            allChapterDataList = chapterResponseData.data;
            Debug.Log("allChaptersDataList loaded " + allChapterDataList.Count);
        }
    }
    public void FetchAllTroopsData()
    {
        ApiController.ListAllTroopsAPI(ListAllTroopsCallBack, SignInWithTwitter.losAcessToken);
    }
    public void ListAllTroopsCallBack(bool status, String response)
    {
        if ((!status))
        {
            return;
        }
        else
        {
            troopsResponseData = JsonConvert.DeserializeObject<TroopsResponseData>(response);
            allTroopsDataList = troopsResponseData.data;
            Debug.Log("allTroopsDataList loaded " + troopsResponseData.data.Count);
        }

    }
    public List<BuildingsData> GetAllBuildingsData()
    {
       return allBuildingsDataList;
    }
    public List<TroopsData> GetAllTroopsData()
    {
        return allTroopsDataList;
    }

    public List<Troop> GetUserTroopsData()
    {
        return userTroopsDataList;
    }
    public List<ChapterData> GetAllChaptersData()
    {
        return allChapterDataList;
    }
    public MainInventory GetUserMainInventoryData()
    {
        return userMainInventoryData;
    }
    public List <ChapterProgress> GetUserChapterProgressData()
    {
        return userChaptersProgressList;
    }
    public List<WarehouseInventory> GetUserWarehouseInventoryData()
    {
        return userWareHouseInventoryDataList;
    }
    public List<Mail> GetUserEmailsData()
    {
        return userMailsDataList;
    }
    public List<Gift> GetUserGiftsData()
    {
        return userGiftsList;
    }
    /// <summary>
    /// can be used as generic callback whenever data is updated
    /// </summary>
    /// <param name="success"></param>
    /// <param name="response"></param>
    public void UpdatedTimeAndResourcesCallback(bool success, string response)
    {
        print(success);
        print(response);
        if (!success) return;
        SignInResponse updatedUserData = new SignInResponse();
        updatedUserData = JsonConvert.DeserializeObject<SignInResponse>(response);
        //TimeManager.instance.SetTime(updatedUserData.data.serverTime);          // not updating server time everytime
        List<Building> buildings = updatedUserData.data.buildings;
        //RefreshAllBuildingsStates(buildings);
        GameManager.instance.userPanelUI.SetUserData(updatedUserData.data);
        DataManager.instance.SetUserData(updatedUserData);
        onUserDataUpdated?.Invoke();
        GameManager.instance.reopenPanel.SetActive(false);
    }
}
