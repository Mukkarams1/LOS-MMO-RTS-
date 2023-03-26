using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUpPanelUI : MonoBehaviour
{
    [SerializeField]
    Timer timer;
    public WareHouseIconPreFab WareHouseInventoryItemPrefab;
    public Transform speedUpPanelInventoryItemContent;
    public Button Closebtn;
    List<WarehouseInventory> userWarehouseInventory = new List<WarehouseInventory>();
    int speedUpItemCategory = (int) RedeemCategory.SpeedUp;
    string uniqueBID;
    string buildingTimerText;


    void UpdateTimerText()
    {
        buildingTimerText = GameManager.instance.buildingManager.buildingPrefabsList.FirstOrDefault(i => i.CurrentBuildingData._id.Equals(uniqueBID))?.GetTimer().timerText.text;
        timer.UpdateTimerTextOnly(buildingTimerText);
    }
    private void OnEnable()
    {
        Closebtn.onClick.AddListener(OnClickClose);
        DataManager.onUserDataUpdated += ResetSpeedUpPanel;
        userWarehouseInventory = DataManager.instance.GetUserWarehouseInventoryData().Where(i => i.redeemCategory.Equals((int)speedUpItemCategory))?.ToList();
        uniqueBID = GameManager.instance.buildingManager.SelectedBuildingUniqueId;

        PopulateSpeedUpPanel();
        StartCoroutine(CustomUpdate());
    }

    void OnDisable() 
    {
        DataManager.onUserDataUpdated -= ResetSpeedUpPanel;
        StopCoroutine(CustomUpdate());
        ClearSpeedUpPanel();
    }
    void ResetSpeedUpPanel()
    {
        userWarehouseInventory = DataManager.instance.GetUserWarehouseInventoryData().Where(i => i.redeemCategory.Equals((int)speedUpItemCategory))?.ToList();
        ClearSpeedUpPanel();
        PopulateSpeedUpPanel();
    }

    private void PopulateSpeedUpPanel()
    {
        for (int j = 0; j < userWarehouseInventory.Count; j++)
        {
            WareHouseIconPreFab wareHouseItemIconPreFab = Instantiate(WareHouseInventoryItemPrefab, speedUpPanelInventoryItemContent).GetComponent<WareHouseIconPreFab>();
            wareHouseItemIconPreFab.SetWareHouseData(userWarehouseInventory[j]);

        }
    }
    private void OnClickClose()
    {
        gameObject.SetActive(false);
        SoundManager.instance.PlaySound("ButtonClick");
    }
    private void ClearSpeedUpPanel()
    {

        foreach (Transform child in speedUpPanelInventoryItemContent)
        {
            Destroy(child.gameObject);
        }
    }

    public IEnumerator CustomUpdate()
    {
        while (true)
        {
            UpdateTimerText();
            yield return null;
        }
    }
}
