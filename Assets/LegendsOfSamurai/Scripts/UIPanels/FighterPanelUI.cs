using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class FighterPanelUI : MonoBehaviour
{
    public Text troopIdText;
    public Text troopTitleText;
    public Text troopDescriptionText;
    public Text troopFoodText;
    public Text troopWoodText;
    public Text troopMetalText;
    public Text troopGunPowderText;
    public Text troopDiamondText;
    public Text troopGoldText;
    public Image Img;
    [SerializeField]
    Slider quantitySlider;
    [SerializeField]
    TextMeshProUGUI troopTimerCost;
    int addedQuantity;
    int currentTroop;
    public Button Next;
    public Button Prev;
    public Button CloseBtn;
    public Button RecruitBtn;
    int reqTimeInSeconds = 0;
    private TroopsData SelectedTroop;
    List<TroopsData> allTroopsData = new List<TroopsData>();
    private void OnEnable()
    {
        allTroopsData = DataManager.instance.GetAllTroopsData();
        currentTroop = 0;
        addedQuantity = 1;
        //.Where(i => i.unlockLevel < DataManager.instance.SignInResponseData.data.level).ToList()
        if (allTroopsData.Count > 0)
            SetTroopDataOnUI(allTroopsData[0]);
        reqTimeInSeconds = allTroopsData[currentTroop].costPerUnit.time * addedQuantity;
    }

    void Start()
    {
        CloseBtn.onClick.AddListener(OnClickClose);
        Debug.Log(DataManager.instance.SignInResponseData.data.userName);
        Next.onClick.AddListener(OnNextClicked);
        Prev.onClick.AddListener(OnPrevClicked);
        RecruitBtn.onClick.AddListener(recruitTroop);
        quantitySlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }
    void ValueChangeCheck()
    {
        addedQuantity = (int)quantitySlider.value;
        var food = SelectedTroop.costPerUnit.food * addedQuantity;
        var wood = SelectedTroop.costPerUnit.wood * addedQuantity;
        var metal = SelectedTroop.costPerUnit.metal * addedQuantity;
        var gunPowder = SelectedTroop.costPerUnit.gunPowder * addedQuantity;
        var diamond = SelectedTroop.costPerUnit.diamond * addedQuantity;
        var gold = SelectedTroop.costPerUnit.gold * addedQuantity;
        reqTimeInSeconds = allTroopsData[currentTroop].costPerUnit.time * addedQuantity;
        UpdateTroopCost(food,wood,metal,gunPowder,diamond,gold);
    }
    private void OnClickClose()
    {
        gameObject.SetActive(false);
        SoundManager.instance.PlaySound("ButtonClick");
    }

    private void OnNextClicked()
    {
        SoundManager.instance.PlaySound("ButtonClick");
        currentTroop++;
        if (currentTroop > allTroopsData.Count - 1)
        {
            currentTroop = 0;

        }
        if (allTroopsData.Count > 0)
            SetTroopDataOnUI(allTroopsData[currentTroop]);
    }

    private void OnPrevClicked()
    {
        SoundManager.instance.PlaySound("ButtonClick");
        currentTroop--;
        if (currentTroop < 0)
        {
            currentTroop = allTroopsData.Count - 1;
        }
        if (allTroopsData.Count > 0)
            SetTroopDataOnUI(allTroopsData[currentTroop]);
    }

    public void SetUIDetails(string troopName, string troopTitle, string troopdescription, int meat, int wood, int metal, int gunPowder, int diamond, int gold, int reqTime , string img)
    {
        troopTitleText.text = troopTitle;
        troopDescriptionText.text = troopdescription;
        if(meat == 0)
        {
            troopFoodText.gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            troopFoodText.gameObject.transform.parent.gameObject.SetActive(true);
            troopFoodText.text = meat.ToString();
        }
        if(wood == 0)
        {
            troopWoodText.gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            troopWoodText.text = wood.ToString();
            troopWoodText.gameObject.transform.parent.gameObject.SetActive(true);
        }
        if(metal == 0)
        {
            troopMetalText.gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            troopMetalText.gameObject.transform.parent.gameObject.SetActive(true);
            troopMetalText.text = metal.ToString();
        }
        if(gunPowder == 0)
        {
            troopGunPowderText.gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            troopGunPowderText.gameObject.transform.parent.gameObject.SetActive(true);
            troopGunPowderText.text = gunPowder.ToString();
        }
        if(diamond == 0)
        {
            troopDiamondText.gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            troopDiamondText.gameObject.transform.parent.gameObject.SetActive(true);
            troopDiamondText.text = diamond.ToString();
        }
        if(gold == 0)
        {
            troopGoldText.gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            troopGoldText.gameObject.transform.parent.gameObject.SetActive(true);
            troopGoldText.text = gold.ToString();
        }
        
        troopTimerCost.text = reqTime.ToString();
        Img.sprite = Resources.Load<Sprite>("TroopImages/" + img );
        RecruitBtn.gameObject.SetActive(allTroopsData[currentTroop].unlockLevel < DataManager.instance.SignInResponseData.data.level);
    }
    private void UpdateTroopCost(int food, int wood, int metal, int gunPowder, int diamond, int gold)
    {
        troopFoodText.text = food.ToString();
        troopWoodText.text = wood.ToString();
        troopMetalText.text = metal.ToString();
        troopGunPowderText.text = gunPowder.ToString();
        troopDiamondText.text = diamond.ToString();
        troopGoldText.text = gold.ToString();
        troopTimerCost.text  = reqTimeInSeconds.ToString();
    }
    public void SetTroopDataOnUI(TroopsData troop)
    {
        quantitySlider.value = quantitySlider.minValue;
        SelectedTroop = troop;
        reqTimeInSeconds = troop.costPerUnit.time * addedQuantity;
        quantitySlider.maxValue = CalculateMaxTroopCount(troop);    
        SetUIDetails(troop._id, troop.title, troop.description, troop.costPerUnit.food, troop.costPerUnit.wood, troop.costPerUnit.metal,
            troop.costPerUnit.gunPowder, troop.costPerUnit.diamond, troop.costPerUnit.gold, reqTimeInSeconds ,troop.image);
    }

    void recruitTroop()
    {
        //GameManager.instance.userTroopUI.AddNewTroop(allTroopsData[currentTroop].title, "0", addedQuantity.ToString());
        ApiController.RecruitTroopAPI(DataManager.instance.UpdatedTimeAndResourcesCallback, SignInWithTwitter.losAcessToken, allTroopsData[currentTroop]._id, addedQuantity);
        var troopBuildingID = GameManager.instance.buildingManager.SelectedBuildingUniqueId;
        GameManager.instance.buildingManager.buildingPrefabsList.Where(i => i.CurrentBuildingData._id.Equals(troopBuildingID)).FirstOrDefault()?.StartTroopTimerCountdown(reqTimeInSeconds); // todo start timer in timer.cs of that building
        GameManager.instance.activeTroopPnl(false);
    }
    int  CalculateMaxTroopCount(TroopsData data)
    {
        List<int> quantities = new List<int>() ;
        MainInventory mainInventory = DataManager.instance.GetUserMainInventoryData();
        if (data.costPerUnit.food != 0)
        {
            var food = Mathf.CeilToInt(mainInventory.food / data.costPerUnit.food);
            quantities.Add(food);
        }
        if(data.costPerUnit.wood != 0)
        {
            var wood = Mathf.CeilToInt(mainInventory.wood / data.costPerUnit.wood);
            quantities.Add(wood);
        }
        if(data.costPerUnit.metal != 0)
        {
            var metal = Mathf.CeilToInt(mainInventory.metal / data.costPerUnit.metal);
            quantities.Add(metal);
        }
        if(data.costPerUnit.gunPowder != 0)
        {
            var gunPowder = Mathf.CeilToInt(mainInventory.gunPowder / data.costPerUnit.gunPowder);
            quantities.Add(gunPowder);
        }
        if(data.costPerUnit.gold != 0)
        {
            var gold = Mathf.CeilToInt(mainInventory.gold / data.costPerUnit.gold);
            quantities.Add(gold);
        }
        if(data.costPerUnit.diamond != 0)
        {
            var diamond = Mathf.CeilToInt(mainInventory.diamond / data.costPerUnit.diamond);
            quantities.Add(diamond);
        }
        if(quantities.Min() >= 50)
        {
            return 50;
        }
        else
        {
            return quantities.Min();
        }
    }
}
