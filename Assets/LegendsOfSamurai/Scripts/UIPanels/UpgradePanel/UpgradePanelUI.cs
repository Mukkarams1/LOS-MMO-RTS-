using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Linq;

public class UpgradePanelUI : MonoBehaviour
{
    public Button CloseBtn;
    public Text foodText;
    public Text woodText;
    public Text metalText;
    public Text gunPowderText;
    public Text GoldText;
    public Text DiomandText;
  
    public Button UpgradeBtn;
    public Text UpgradeBtnTxt;
    public Text ReasonText;
    Building selectedBuilding;
    BuildingsData selectedbuildingGenericData;
    [SerializeField]
    Image BuildingImage;
    public List<BuildingsData> allBuildingsList = new List<BuildingsData>();
    void Start()
    {
        CloseBtn.onClick.AddListener(OnClickClose);
        UpgradeBtn.onClick.AddListener(UpgradeBuilding);
    }
    private void OnEnable()
    {
        allBuildingsList = DataManager.instance.GetAllBuildingsData();
        SetUpgradePanel();
    }

    public void SetUpgradePanel()
    {
        ReasonText.gameObject.SetActive(false);
        selectedBuilding = DataManager.instance.GetUserBuildingsData().Where(i => i._id.Equals(GameManager.instance.buildingManager.SelectedBuildingUniqueId)).FirstOrDefault();
        selectedbuildingGenericData = allBuildingsList.Where(i => i._id.Equals(selectedBuilding.buildingId)).FirstOrDefault();
        var costPerLevelList = GetCostperLevelDataList();
        BuildingImage.sprite = Resources.Load<Sprite>("BuildingImage/" + selectedbuildingGenericData.image);

        if (selectedBuilding.currentLevel >= costPerLevelList.Count-1)
        {
            UpgradeBtn.gameObject.SetActive(false);
            ReasonText.gameObject.SetActive(true);
            ReasonText.text = "Max Level Reached";
        }
        else
        {

            SetUpgradeUi(costPerLevelList[selectedBuilding.currentLevel]); // since level is not starting from 0 so level cost for next level will be at currentlevel index
            UpgradeBtn.gameObject.SetActive(true);
        }
    }
    public List<CostPerLevel> GetCostperLevelDataList()
    {
        var costPerLevelData = selectedbuildingGenericData?.costPerLevel;
        return costPerLevelData;
    }

    public void SetUpgradeUi(CostPerLevel costPerLevel)
    {
        var mainInventory = DataManager.instance.GetUserMainInventoryData();
        bool isFoodGreater = mainInventory.food >= costPerLevel.food;
        bool isWoodGreater = mainInventory.wood >= costPerLevel.wood;
        bool isMetalGreater = mainInventory.metal >= costPerLevel.metal;
        bool isGunPowderGreater = mainInventory.gunPowder >= costPerLevel.gunPowder;
        bool isGoldGreater = mainInventory.gold >= costPerLevel.gold;
        bool isDiamondGreater = mainInventory.diamond >= costPerLevel.diamond;
        if (isFoodGreater)
        {
            foodText.text = costPerLevel.food+" / "+costPerLevel.food;    
            foodText.color = Color.green;    
        }
        else
        {
            foodText.text = mainInventory.food + " / " + costPerLevel.food;
            foodText.color = Color.red;
        }
        if (isWoodGreater)
        {
            woodText.text = costPerLevel.wood + " / " + costPerLevel.wood;
            woodText.color = Color.green;
        }
        else
        {
            woodText.text = mainInventory.wood + " / " + costPerLevel.wood;
            woodText.color = Color.red;
        }
        if (isMetalGreater)
        {
            metalText.text = costPerLevel.metal + " / " + costPerLevel.metal;
            metalText.color = Color.green;
        }
        else
        {
            metalText.text = mainInventory.metal + " / " + costPerLevel.metal;
            metalText.color = Color.red;
        }
        if (isGunPowderGreater)
        {
            gunPowderText.text = costPerLevel.gunPowder + " / " + costPerLevel.gunPowder;
            gunPowderText.color = Color.green;
        }
        else
        {
            gunPowderText.text = mainInventory.gunPowder + " / " + costPerLevel.gunPowder;
            gunPowderText.color = Color.red;
        }
        if (isGoldGreater)
        {
            GoldText.text = costPerLevel.gold + " / " + costPerLevel.gold;
            GoldText.color = Color.green;
        }
        else
        {
            GoldText.text = mainInventory.gold + " / " + costPerLevel.gold;
            GoldText.color = Color.red;
        }
        if (isDiamondGreater)
        {
            DiomandText.text = costPerLevel.diamond + " / " + costPerLevel.diamond;
            DiomandText.color = Color.green;
        }
        else
        {
            DiomandText.text = mainInventory.diamond + " / " + costPerLevel.diamond;
            DiomandText.color = Color.red;
        }

        if (isFoodGreater && isWoodGreater && isMetalGreater && isGunPowderGreater && isGoldGreater &&  isDiamondGreater)
        {
            UpgradeBtn.interactable = true;
            var nextLevel = selectedBuilding.currentLevel + 1;
            UpgradeBtnTxt.text = "upgrade to level " + nextLevel.ToString();
        }
        else
        {
            UpgradeBtn.interactable = false;
            UpgradeBtnTxt.text = "insufficient Resources";
        }


    }

    private void OnClickClose()
    {
        gameObject.SetActive(false);
        SoundManager.instance.PlaySound("ButtonClick");
    }

    void UpgradeBuilding()
    {
        GameManager.instance.buildingManager.InstantUpgradeBuildingState(GameManager.instance.buildingManager.SelectedBuildingUniqueId, EnumBuildingStates.upgrade);
        OnClickClose();
    }


}

