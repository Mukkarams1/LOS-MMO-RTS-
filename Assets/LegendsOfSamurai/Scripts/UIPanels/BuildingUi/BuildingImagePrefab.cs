using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class BuildingImagePrefab : MonoBehaviour
{
    BuildingsData buildingsData;
    BuildingsPanelUI buildingsPanel;
    public Image BuildingImage;
    public Button prefabbtn;
    public Text LockingReason;
    public Text DimondText;
    public Text FoodText;
    public Text GoldText;
    public Text GunPowderText;
    public Text MetalText;
    public Text WoodText;
    public Text TimeText;
    public Text Title;
    int userlevel;

    private void Start()
    {
      prefabbtn.GetComponent<Button>().onClick.AddListener(OnBuildingPrefabCLicked);
    }
    void OnBuildingPrefabCLicked()
    {
        buildingsPanel.OnBuildingImageClicked(buildingsData);
        buildingsPanel.gameObject.SetActive(false);
        //BuildingPrefab.instance.Interacted();
        SoundManager.instance.PlaySound("ButtonClick");
    }
    public void SetBuildingData(BuildingsData data, BuildingsPanelUI buildingPanelUi)
    {
        MainInventory inventory = DataManager.instance.SignInResponseData.data.mainInventory;
        userlevel = DataManager.instance.SignInResponseData.data.level;
        buildingsData = data;
        buildingsPanel = buildingPanelUi;
        UpdateUI();
        if (data.unlockLevel > DataManager.instance.SignInResponseData.data.level ||
            data.costPerLevel[0].diamond > inventory.diamond  || data.costPerLevel[0].food > inventory.food || data.costPerLevel[0].gold > inventory.gold       || data.costPerLevel[0].gunPowder > inventory.gunPowder || data.costPerLevel[0].metal > inventory.metal
            || DataManager.instance.GetUserBuildingsData().Where(i => i.buildingId.Equals(data._id)).FirstOrDefault() != null)
        {
            prefabbtn.interactable = false;
            BuildingImage.color = new Color32(147, 147, 147, 147);
            if(data.unlockLevel > DataManager.instance.SignInResponseData.data.level)
            {
                LockingReason.gameObject.SetActive(true);
                LockingReason.text = ("Requires Level " + data.unlockLevel).ToString();
            }
            else if (DataManager.instance.GetUserBuildingsData().Where(i => i.buildingId.Equals(data._id)).FirstOrDefault() != null)
            {
                LockingReason.gameObject.SetActive(true);
                LockingReason.text = ("Building already exist").ToString();
            }
            else if(data.costPerLevel[0].diamond > inventory.diamond || data.costPerLevel[0].food > inventory.food || data.costPerLevel[0].gold > inventory.gold || data.costPerLevel[0].gunPowder > inventory.gunPowder || data.costPerLevel[0].metal > inventory.metal)
            {
                LockingReason.gameObject.SetActive(true);
                LockingReason.text = ("Insufficient resources").ToString();
            }
            else
            {
                LockingReason.gameObject.SetActive(false);
            }
        }
        else
        {
            LockingReason.gameObject.SetActive(false);
        }

    }

    private void UpdateUI()
    {
        BuildingImage.sprite = Resources.Load<Sprite>("BuildingImage/" + buildingsData.image);
        if(buildingsData.costPerLevel[0] != null)
        {
            DimondText.text = (buildingsData.costPerLevel[0].diamond).ToString();
            FoodText.text = (buildingsData.costPerLevel[0].food).ToString();
            GoldText.text = (buildingsData.costPerLevel[0].gold).ToString();
            GunPowderText.text = (buildingsData.costPerLevel[0].gunPowder).ToString();
            MetalText.text = (buildingsData.costPerLevel[0].metal).ToString();
            WoodText.text = (buildingsData.costPerLevel[0].wood).ToString();
            TimeText.text = ("Time:" + buildingsData.costPerLevel[0].time).ToString();
            Title.text = (buildingsData.title).ToString();
        }
        
    }
}
