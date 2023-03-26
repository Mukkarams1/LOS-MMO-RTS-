using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GiftInventoryUIPrefab : MonoBehaviour
{
    GiftInventory giftInventory;
    public Text title;
    public Text unitvalue;
    public Text quantity;
    public Text description;
    public Image img;
 
    public void SetUI(GiftInventory giftInventory)
    {
        this.giftInventory = giftInventory;
        title.text = giftInventory.title;
        unitvalue.text = giftInventory.unitValue;
        quantity.text = giftInventory.quantity;
        description.text = giftInventory.description;
        CategorilyCustomizeUI();
    }
    private void CategorilyCustomizeUI()
    {
        int category,Inventory;
        int.TryParse(giftInventory.redeemCategory,out category);
        int.TryParse(giftInventory.inventoryType,out Inventory);
        switch ((RedeemCategory) category)
        {
            case RedeemCategory.WareHouse:
                switch (Inventory)
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
                img.sprite = DataManager.instance.boosterBundleSprite;
                break;
            case RedeemCategory.SpeedUp:
                img.sprite = DataManager.instance.speedUpBundleSprite;

                break;
            default:
                break;
        }
    }
}
