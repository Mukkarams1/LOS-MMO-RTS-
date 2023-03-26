using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingsPanelUI : MonoBehaviour
{
    public int currentCategory;
    public GameObject BuildingPrefab;
    public Transform BuildingPrefabPlaceHolder;
    public Button FirstSelectedButton;
  //  public Button CloseBtn;
  //  public Text shortDescriptionTxt;
  //  public Text TitleTxt;
  //  public Text WoodTxt;
  //  public Text MeatTxt;
  //  public Text MetalTxt;
  //  public Text YieldPerHourTxt;
  //  public Image Img;
  //  public GameObject ScrollBar;
   List<BuildingsData> allBuildingsList = new List<BuildingsData>();

    internal void OnBuildingImageClicked(BuildingsData data)
    {
        GameManager.instance.buildingManager.CreateNewBuildingPressed(data);
  //      shortDescriptionTxt.text = data.shortDescription;
  //      TitleTxt.text = data.title;
  //      if (data.costPerLevel.Count > 0)
  //      {
  //          WoodTxt.text = data.costPerLevel[0].wood.ToString();
  //          MetalTxt.text = data.costPerLevel[0].metal.ToString();
  //          MeatTxt.text = data.costPerLevel[0].food.ToString();
  //          YieldPerHourTxt.text = data.costPerLevel[0].yieldPerHour.ToString();
  //      }

 //       Img.sprite = Resources.Load<Sprite>("BuildingImage/" + data.image);

    }
  //  public void ClosePanel()
   // {
  //      gameObject.SetActive(false);
  //  }
    private void OnEnable()
    {
        if (DataManager.instance!=null)
        {
        allBuildingsList = DataManager.instance.GetAllBuildingsData();
        print("allBuildingsDataList: " + allBuildingsList.Count);
        }
        SetCategoryToResources();    /// set category after buildings list is downloaded from server
    }
    private void Start()
    {
      //  CloseBtn.onClick.AddListener(OnClickClose);


        FirstSelectedButton.Select();
    }

    private void OnClickClose()
    {

        gameObject.SetActive(false);
        SoundManager.instance.PlaySound("ButtonClick");
    }

    public void SetBuildingBycategory()
    {

        for (int i = 0; i < allBuildingsList.Count; i++)
        {
            if (allBuildingsList[i].category == currentCategory)
            {
                BuildingImagePrefab building = Instantiate(BuildingPrefab, BuildingPrefabPlaceHolder).GetComponent<BuildingImagePrefab>();
                building.SetBuildingData(allBuildingsList[i], this);
            }
        }
    }
    public void ClearPreObj()
    {
        foreach (Transform child in BuildingPrefabPlaceHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public void SetCategoryToResources()
    {
        currentCategory = 1;
        ClearPreObj();
        SetBuildingBycategory();
       // SetScroolBar();
    }
    public void SetCategoryToWarFare()
    {
        currentCategory = 2;
        ClearPreObj();
        SetBuildingBycategory();
      //  SetScroolBar();
    }
    public void SetCategoryToStartage()
    {
        currentCategory = 3;
        ClearPreObj();
        SetBuildingBycategory();
      //  SetScroolBar();
    }

    //public void SetScroolBar()
    //{
    //    if (BuildingPrefabPlaceHolder.childCount > 6)
    //    {
    //        ScrollBar.SetActive(true);
    //    }
    //    else
    //    {
    //        ScrollBar.SetActive(false);
    //    }
    //}

}
