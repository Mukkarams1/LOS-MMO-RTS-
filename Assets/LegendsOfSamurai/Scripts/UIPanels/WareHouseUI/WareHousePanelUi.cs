using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WareHousePanelUi : MonoBehaviour
{
    public GameObject WareHousePrefab;
    public Transform wareHousePlaceHolder;
    public GameObject scrollBar;
    public Button Closebtn;
    List<WarehouseInventory> userWarehouseInventory = new List<WarehouseInventory>();
    void Start()
    {
        Closebtn.onClick.AddListener(OnClickClose);

    }

    private void OnClickClose()
    {
        gameObject.SetActive(false);
        SoundManager.instance.PlaySound("ButtonClick");
    }
    private void OnEnable()
    {
        userWarehouseInventory = DataManager.instance.GetUserWarehouseInventoryData().Where(i=>i.redeemCategory.Equals((int)RedeemCategory.WareHouse)).ToList();
        PopulateWareHouseInventory();

    }
    private void OnDisable()
    {
        ClearWareInventoryPanel();
    }
    private void PopulateWareHouseInventory()
    {
     
           
                for (int j = 0; j < userWarehouseInventory.Count; j++)
                {
                    WareHouseIconPreFab warehouseEntryObject = Instantiate(WareHousePrefab, wareHousePlaceHolder).GetComponent<WareHouseIconPreFab>();
                    warehouseEntryObject.SetWareHouseData(userWarehouseInventory[j]);

                }
                SetScroolBar();
                return;
    }
    private void ClearWareInventoryPanel()
    {
   
            foreach (Transform child in wareHousePlaceHolder)
            {
                Destroy(child.gameObject);
            }
        
    }
    public void SetScroolBar()
    {
        if (wareHousePlaceHolder.childCount > 10)
        {
            scrollBar.SetActive(true);
        }
        else
        {
            scrollBar.SetActive(false);
        }
    }

}
