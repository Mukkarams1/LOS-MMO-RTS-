using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WareHouseIconPreFab : MonoBehaviour
{
    WarehouseInventory warehouseInventory;
    public Text title;
    public Text quantity;
    public Text Description;
    public Image img;
    [SerializeField]
    Button AddButton;
    [SerializeField]
    Slider quantitySlider;
    int itemQuantity = 1;
    


    // Start is called before the first frame update
    RedeemCategory category;
    private void Start()
    {
        AddButton.onClick.AddListener(AddBtnPressed);
        quantitySlider.onValueChanged.AddListener(delegate { SetQuantity(); });
    }
    void SetQuantity()
    {
        itemQuantity = (int)quantitySlider.value;
    }
    void AddBtnPressed()
    {
        switch (category)
        {
            case RedeemCategory.SpeedUp:
                var SelectedBuildingUniqueId = GameManager.instance.buildingManager.SelectedBuildingUniqueId;
                var qty = warehouseInventory.unitValue * itemQuantity;
                GameManager.instance.buildingManager.buildingPrefabsList.FirstOrDefault(i => i.CurrentBuildingData._id.Equals(SelectedBuildingUniqueId))?.GetTimer().AddSpeedUpItemEffect(warehouseInventory.unitValue * itemQuantity);
                ApiController.SpeedUpBuildingAPI(DataManager.instance.UpdatedTimeAndResourcesCallback,SignInWithTwitter.losAcessToken, SelectedBuildingUniqueId, warehouseInventory._id, itemQuantity);
                break;

            case RedeemCategory.Booster:
                var existingBooster = DataManager.instance.GetUserAppliedBoosters()?.FirstOrDefault(i => i.inventoryType.Equals(warehouseInventory.inventoryType) && i.expiry > TimeManager.instance.serverTime);
                if (existingBooster!=null)
                {
                    //show warning that this booster will be replaced by new booster 
                    GameManager.instance.ShowNotificationPopUpUI("you already had a same booster which is replaced.");
                }
                ApiController.BuildingBoosterAPI(DataManager.instance.UpdatedTimeAndResourcesCallback,SignInWithTwitter.losAcessToken,warehouseInventory._id);
                break;

            default:
                ApiController.AddWarehouseInventoryAPI(DataManager.instance.UpdatedTimeAndResourcesCallback, SignInWithTwitter.losAcessToken, warehouseInventory._id, itemQuantity);
                break;
        }
        StartCoroutine(AddBtnInteractableAfterDelay());
        GameManager.instance.ToggleWareHousePanel(false);
        GameManager.instance.creatGiftRedeemAmin(AddButton.transform);
    }
    public void SetWareHouseData(WarehouseInventory data)
    {
        warehouseInventory = data;
        quantitySlider.maxValue = data.quantity;
        category = (RedeemCategory)data.redeemCategory;
        if (category== RedeemCategory.Booster)
        {
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        Description.text = warehouseInventory.description;
        title.text = warehouseInventory.title;
        quantity.text = warehouseInventory.quantity.ToString();
        CategorilyCustomizeUI();
    }
    private void CategorilyCustomizeUI()
    {
        switch (category)
        {
            case RedeemCategory.WareHouse:
                switch (warehouseInventory.inventoryType)
                {
                    case 1:
                        img.sprite = DataManager.instance.foodBundleSprite;

                        break;
                    case 2:
                        img.sprite = DataManager.instance.woodBundleSprite;

                        break;
                    case 3:
                        img.sprite = DataManager.instance.metalBundleSprite;

                        break;
                    case 4:
                        img.sprite = DataManager.instance.gunPowderBundleSprite;

                        break;
                    case 5:
                        img.sprite = DataManager.instance.goldBundleSprite;

                        break;
                    case 6:
                        img.sprite = DataManager.instance.diamondBundleSprite;

                        break;
                    default:
                        break;
                }
                break;
            case RedeemCategory.Booster:
                quantitySlider.gameObject.SetActive(false);
                img.sprite = DataManager.instance.boosterBundleSprite;
                break;
            case RedeemCategory.SpeedUp:
                img.sprite = DataManager.instance.speedUpBundleSprite;

                break;
            default:
                break;
        }
    }
    public IEnumerator AddBtnInteractableAfterDelay()
    {
        AddButton.interactable = false;
        yield return new WaitForSeconds(3f);
        AddButton.interactable = true;
    }
}

public enum RedeemCategory
{
    WareHouse = 1,
    Booster = 2,
    SpeedUp = 3,
}