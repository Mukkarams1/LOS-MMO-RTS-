using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBtnUi : MonoBehaviour
{
  
    public GameObject OnClickBtns;
    public GameObject UpgradeBtn;
    public GameObject RecuirterBtn;
    public GameObject ResearchBtn;
    public GameObject SpeedupBtn;
    public GameObject ConstructBtn;
    public GameObject ViewBtn;
    public Transform[] buldingObject;
    public Transform PlaceHolder;
    public void setOnClickBtns(BuildingPrefab buildingPrefab , int category)
    {
        OnClickBtns.transform.position = Camera.main.WorldToScreenPoint(buildingPrefab.transform.position);
        clearPlaceHolders();

        //All buildings except decoration buildings will be upgradable
        if (category != 4) {
            Instantiate(UpgradeBtn, PlaceHolder);
        }

        //All buildings under construction will have a speed up button except decoration buildings
        if (category != 4 && buildingPrefab.currentBuildingStatus == EnumBuildingStates.construct || buildingPrefab.currentBuildingStatus == EnumBuildingStates.upgrade )
        {
            Instantiate(SpeedupBtn, PlaceHolder);
        }

        //All buildings will have view button
        Instantiate(ViewBtn, PlaceHolder);

        //Only warfare buildings will have recruit button
        if (category == 2)
        {
            Instantiate(RecuirterBtn, PlaceHolder);
        }

        //Only research buildings will have research button
        //else if ( category == 3)
        //{
        //    Instantiate(ResearchBtn, PlaceHolder);
        //}
    }

    public void clearPlaceHolders()
    {
      
            foreach (Transform child in PlaceHolder)
            {
                GameObject.Destroy(child.gameObject);
            }
        
    }
}
