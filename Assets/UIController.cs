using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public GameObject unlockZonePanel;
    public GameObject createBuildingWorldPanel;
    public GameObject createBuildingPanel;
    public GameObject lockSprite;
    public GameObject buildingSprite;
    public GameObject tileSprite;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void lockPressed()
    {
        unlockZonePanel.SetActive(true);
    }
    public void UnlockZone() {
        unlockZonePanel.SetActive(false);
        lockSprite.SetActive(false);
    }
    public void CreateBuildingPressed() {
        createBuildingWorldPanel.SetActive(true);
        float z = createBuildingWorldPanel.transform.position.z;
        createBuildingWorldPanel.transform.position = new Vector3(tileSprite.transform.position.x, tileSprite.transform.position.y, z); ;
    }
    public void ShowCreateBuildingPanel()
    {
        createBuildingPanel.SetActive(true);
        
    }
    public void ConstructBuilding()
    {
        createBuildingPanel.SetActive(false);
        buildingSprite.SetActive(true);
    }
}