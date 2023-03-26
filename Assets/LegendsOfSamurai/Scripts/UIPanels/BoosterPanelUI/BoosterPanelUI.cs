using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BoosterPanelUI : MonoBehaviour
{
    public WareHouseIconPreFab wareHouseInventoryItemPrefab;
    public Transform boosterPanelInventoryItemContent;
    public Button Closebtn;
    List<WarehouseInventory> userWarehouseInventory = new List<WarehouseInventory>();
    int boosterItemCategory =(int) RedeemCategory.Booster;
    string uniqueBID;
    string buildingTimerText;
    void Start()
    {
        Closebtn.onClick.AddListener(OnClickClose);
        uniqueBID = GameManager.instance.buildingManager.SelectedBuildingUniqueId;

    }
 
    private void OnEnable()
    {
        DataManager.onUserDataUpdated += ResetBoosterPanel;

        userWarehouseInventory = DataManager.instance.GetUserWarehouseInventoryData().Where(i=>i.redeemCategory.Equals((int)boosterItemCategory))?.ToList();
        PopulateBoosterPanel();

    }
    private void OnDisable()
    {
        DataManager.onUserDataUpdated -= ResetBoosterPanel;

        ClearBoosterPanel();
    }
    void ResetBoosterPanel()
    {
        userWarehouseInventory = DataManager.instance.GetUserWarehouseInventoryData().Where(i => i.redeemCategory.Equals((int)boosterItemCategory))?.ToList();
        ClearBoosterPanel();
        PopulateBoosterPanel();
    }
    private void OnClickClose()
    {
        gameObject.SetActive(false);
        SoundManager.instance.PlaySound("ButtonClick");
    }
    private void PopulateBoosterPanel()
    {
        for (int j = 0; j < userWarehouseInventory.Count; j++)
        {
            WareHouseIconPreFab wareHouseItemPrefab = Instantiate(wareHouseInventoryItemPrefab, boosterPanelInventoryItemContent).GetComponent<WareHouseIconPreFab>();
            wareHouseItemPrefab.SetWareHouseData(userWarehouseInventory[j]);
        }
    }
    private void ClearBoosterPanel()
    {
        foreach (Transform child in boosterPanelInventoryItemContent)
        {
            Destroy(child.gameObject);
        }
    }

}
