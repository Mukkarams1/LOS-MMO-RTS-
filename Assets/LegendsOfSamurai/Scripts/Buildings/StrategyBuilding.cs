using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrategyBuilding : MonoBehaviour
{
    public BuildingBtnUi buildingBtnUi;
    private int Category = 3;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnBuildingClicked);
    }
    void OnBuildingClicked()
    {
       // GameManager.instance.buildingButtonsPanel.setOnClickBtns(transform, Category);

    }
}
