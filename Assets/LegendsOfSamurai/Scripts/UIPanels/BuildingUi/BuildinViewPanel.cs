using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuildinViewPanel : MonoBehaviour
{
    [SerializeField]
    Image BuildingImage;
    [SerializeField]
    Text titleText;
    [SerializeField]
    Text shortDescText;
    [SerializeField]
    Text LongDescText;
    [SerializeField]
    Text currentLevelText;
    [SerializeField]
    Text NextLeveltext;
    [SerializeField]
    GameObject infoPanel; // first panel GameObject
    [SerializeField]
    GameObject detailsPanel; // second panel GameObject
   
    [SerializeField]
    Button infoButton; // button to show if panel is active
    [SerializeField]
    Button detailsButton; // button to show if panel is active
    [SerializeField]
    Transform ContentTransformForEntries; // content for entries
    [SerializeField]
    GameObject CostPerLevelEntries; // cost per level details
    string selectedBuildingGenericID;
    string selectedBuildingUniqueID;
    BuildingsData genricBuildingData;
    Building userBuildingData;
    ColorBlock NormalClr;
    ColorBlock GreyedOut;
    void OnEnable()
    {
        selectedBuildingGenericID = GameManager.instance.buildingManager.SelectedBuildingidForClick;
        selectedBuildingUniqueID = GameManager.instance.buildingManager.SelectedBuildingUniqueId;
        genricBuildingData = GetBuildingGenericData();
        userBuildingData = GetUserBuildingData();
        InitiazlizeButtonColors();
        // set panel1 to be active by default
        infoPanel.SetActive(true);
        infoButton.colors = NormalClr;

        detailsPanel.SetActive(false);
        detailsButton.colors = GreyedOut;
        // add listener to toggleButton's onClick event
        infoButton.onClick.AddListener(ToggleInfoPanel);
        detailsButton.onClick.AddListener(ToggleDetailsPanel);
        SetUIData();
    }
    void InitiazlizeButtonColors()
    {
        NormalClr = infoButton.colors;
        GreyedOut = NormalClr;
        GreyedOut.disabledColor = Color.grey;
        GreyedOut.highlightedColor = Color.grey;
        GreyedOut.selectedColor = Color.grey;
        GreyedOut.normalColor = Color.grey;
        GreyedOut.pressedColor = Color.grey;

    }
    private void ToggleDetailsPanel()
    {
       
            infoPanel.SetActive(false);
            detailsPanel.SetActive(true);
        infoButton.colors = GreyedOut;
        detailsButton.colors = NormalClr;


    }
    void ToggleInfoPanel()
    {
        infoPanel.SetActive(true);
        detailsPanel.SetActive(false);
        detailsButton.colors = GreyedOut;        
        infoButton.colors = NormalClr;        
    }

    private void SetUIData()
    {

        BuildingImage.sprite = Resources.Load<Sprite>("BuildingImage/" + genricBuildingData.image);
        titleText.text = genricBuildingData.title;
        shortDescText.text = genricBuildingData.shortDescription;
        LongDescText.text = genricBuildingData.description;
        currentLevelText.text = userBuildingData.currentLevel.ToString();
        NextLeveltext.text = CheckNextLevel();
        InstantiateEntries();
    }
    private BuildingsData GetBuildingGenericData()
    {
        return DataManager.instance.GetAllBuildingsData().Where(i => i._id.Equals(selectedBuildingGenericID)).FirstOrDefault();
    }
    private Building GetUserBuildingData()
    {
        return DataManager.instance.GetUserBuildingsData().Where(i => i._id.Equals(selectedBuildingUniqueID)).FirstOrDefault();
    }
    private string CheckNextLevel()
    {
        int totalLevel = genricBuildingData.costPerLevel.Count;
        int currentLevel = userBuildingData.currentLevel;
        return currentLevel < totalLevel ? (currentLevel + 1).ToString() : "Nil";
    }
    private void InstantiateEntries()
    {
        int category = genricBuildingData.category;
        for (int i =0; i< genricBuildingData.costPerLevel.Count;i++)
        {
           GameObject costperLevelEntry = Instantiate(CostPerLevelEntries, ContentTransformForEntries);
            var benefit = category == 1 ? genricBuildingData.costPerLevel[i].yieldPerHour : genricBuildingData.costPerLevel[i].power;
            costperLevelEntry.GetComponent<BuildingViewPanelEntry>().setUI((i + 1).ToString(), genricBuildingData.costPerLevel[i].levelNumber.ToString(), benefit.ToString());
        }
    }
    private void OnDisable()
    {
        clearUI();
    }
    void clearUI()
    {
        infoPanel.SetActive(false);
        infoButton.colors = NormalClr;
        detailsPanel.SetActive(false);
        detailsButton.colors = NormalClr;
        foreach (Transform child in ContentTransformForEntries)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
